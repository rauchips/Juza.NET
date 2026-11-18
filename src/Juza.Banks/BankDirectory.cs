using System.Text.Json;
using System.Text.Json.Serialization;

namespace Juza.Banks;

/// <summary>
/// Read-only directory of Kenyan banks, loaded from the data file embedded in this package.
/// </summary>
public sealed class BankDirectory
{
    private const string ResourceName = "Juza.Banks.banks.json";

    private static readonly JsonSerializerOptions JsonOptions = CreateOptions();

    private static readonly Lazy<BankDirectory> DefaultInstance = new(LoadEmbedded);

    private readonly Dictionary<string, Bank> _byCode;
    private readonly Dictionary<string, Bank> _byId;

    private BankDirectory(IReadOnlyList<Bank> banks)
    {
        All = banks;
        _byCode = banks.ToDictionary(b => b.Code, StringComparer.Ordinal);
        _byId = banks.ToDictionary(b => b.Id, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>Gets the directory shipped with this package.</summary>
    public static BankDirectory Default => DefaultInstance.Value;

    /// <summary>Gets every institution, ordered by bank code.</summary>
    public IReadOnlyList<Bank> All { get; }

    /// <summary>Parses a directory from JSON in the same shape as the embedded file.</summary>
    /// <param name="json">The JSON document.</param>
    /// <returns>The parsed directory.</returns>
    public static BankDirectory FromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json);
        var file = JsonSerializer.Deserialize<BankFile>(json, JsonOptions)
            ?? throw new InvalidDataException("Bank directory JSON was empty.");
        var banks = file.Banks.OrderBy(b => b.Code, StringComparer.Ordinal).ToList();
        return new BankDirectory(banks);
    }

    /// <summary>Finds an institution by its two-digit bank code.</summary>
    /// <param name="code">The code, for example "01". A value like "1" is not padded for you.</param>
    /// <returns>The institution, or <see langword="null"/> when the code is not in the directory.</returns>
    public Bank? FindByCode(string? code)
    {
        if (code is null)
        {
            return null;
        }

        return _byCode.GetValueOrDefault(code.Trim());
    }

    /// <summary>Finds an institution by its stable identifier.</summary>
    /// <param name="id">The identifier, case-insensitive.</param>
    /// <returns>The institution, or <see langword="null"/> when not found.</returns>
    public Bank? FindById(string? id)
    {
        if (id is null)
        {
            return null;
        }

        return _byId.GetValueOrDefault(id.Trim());
    }

    /// <summary>Searches names, short names, aliases and codes (case-insensitive substring match).</summary>
    /// <param name="text">The text to look for.</param>
    /// <returns>Matching institutions, ordered by bank code. Empty when the text is blank.</returns>
    public IReadOnlyList<Bank> Search(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        var needle = text.Trim();
        return All.Where(b => Matches(b, needle)).ToList();
    }

    /// <summary>Returns the institutions that are confirmed Pesalink participants.</summary>
    /// <returns>Institutions with <see cref="Participation.Yes"/>.</returns>
    public IReadOnlyList<Bank> PesalinkParticipants() =>
        All.Where(b => b.Pesalink == Participation.Yes).ToList();

    private static bool Matches(Bank bank, string needle) =>
        bank.Code.Equals(needle, StringComparison.Ordinal)
        || bank.Name.Contains(needle, StringComparison.OrdinalIgnoreCase)
        || (bank.ShortName?.Contains(needle, StringComparison.OrdinalIgnoreCase) ?? false)
        || (bank.Aliases?.Any(a => a.Contains(needle, StringComparison.OrdinalIgnoreCase)) ?? false);

    private static JsonSerializerOptions CreateOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        return options;
    }

    private static BankDirectory LoadEmbedded()
    {
        using var stream = typeof(BankDirectory).Assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{ResourceName}' was not found.");
        using var reader = new StreamReader(stream);
        return FromJson(reader.ReadToEnd());
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by System.Text.Json.")]
    private sealed record BankFile(IReadOnlyList<Bank> Banks);
}
