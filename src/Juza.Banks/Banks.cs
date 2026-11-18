namespace Juza.Banks;

/// <summary>The kind of institution a directory entry represents.</summary>
public enum BankType
{
    /// <summary>A licensed commercial bank.</summary>
    Commercial,

    /// <summary>A mortgage finance institution.</summary>
    MortgageFinance,

    /// <summary>A microfinance bank.</summary>
    Microfinance,

    /// <summary>The central bank.</summary>
    CentralBank,

    /// <summary>Any other institution that carries a bank code.</summary>
    Other
}

/// <summary>Licensing state of an institution.</summary>
public enum BankStatus
{
    /// <summary>Operating.</summary>
    Active,

    /// <summary>Merged into another institution.</summary>
    Merged,

    /// <summary>Closed or under liquidation.</summary>
    Closed,

    /// <summary>Not yet checked against an authoritative source.</summary>
    Unverified
}

/// <summary>Whether an institution takes part in Pesalink.</summary>
public enum Participation
{
    /// <summary>Not known. This is the default; absence of evidence is not "no".</summary>
    Unknown,

    /// <summary>Participates.</summary>
    Yes,

    /// <summary>Does not participate.</summary>
    No,
}

/// <summary>A Kenyan bank or bank-coded institution.</summary>
/// <param name="Id">Stable, lowercase kebab-case identifier. Never reused.</param>
/// <param name="Name">Registered or commonly published name.</param>
/// <param name="ShortName">Common short name, if any.</param>
/// <param name="Code">Two-digit bank code (a string, so leading zeros survive).</param>
/// <param name="Type">Kind of institution.</param>
/// <param name="Status">Licensing state.</param>
/// <param name="Pesalink">Pesalink participation.</param>
/// <param name="Aliases">Other names the institution is known by.</param>
public sealed record Bank(
    string Id,
    string Name,
    string? ShortName,
    string Code,
    BankType Type,
    BankStatus Status,
    Participation Pesalink,
    IReadOnlyList<string>? Aliases);
