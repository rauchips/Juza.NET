using System.Text.RegularExpressions;
using Xunit;

namespace Juza.Banks.Tests;

public sealed partial class BankDirectoryTests
{
    private static BankDirectory Directory => BankDirectory.Default;

    [Fact]
    public void DirectoryIsNotEmpty() => Assert.NotEmpty(Directory.All);

    [Fact]
    public void CodesAreTwoDigitsAndUnique()
    {
        Assert.All(Directory.All, b => Assert.Matches(MyRegex(), b.Code));
        Assert.Equal(Directory.All.Count, Directory.All.Select(b => b.Code).Distinct().Count());
    }

    [Fact]
    public void IdsAreKebabCaseAndUnique()
    {
        Assert.All(Directory.All, b => Assert.Matches(new Regex("^[a-z0-9]+(-[a-z0-9]+)*$"), b.Id));
        Assert.Equal(Directory.All.Count, Directory.All.Select(b => b.Id).Distinct().Count());
    }

    [Fact]
    public void FindByCodeKeepsLeadingZero()
    {
        var bank = Directory.FindByCode("01");
        Assert.NotNull(bank);
        Assert.Equal("kcb", bank.Id);
    }

    [Fact]
    public void FindByCodeReturnsNullForUnknownOrNull()
    {
        Assert.Null(Directory.FindByCode("99"));
        Assert.Null(Directory.FindByCode(null));
    }

    [Fact]
    public void FindByIdIsCaseInsensitive() =>
        Assert.Equal("68", Directory.FindById("EQUITY")?.Code);

    [Fact]
    public void SearchMatchesNameCaseInsensitively() =>
        Assert.Contains(Directory.Search("equity"), b => b.Code == "68");

    [Fact]
    public void SearchWithBlankTextReturnsNothing() =>
        Assert.Empty(Directory.Search("  "));

    [Fact]
    public void PesalinkIsUnknownUntilVerified()
    {
        Assert.All(Directory.All, b => Assert.Equal(Participation.Unknown, b.Pesalink));
        Assert.Empty(Directory.PesalinkParticipants());
    }

    [Fact]
    public void FromJsonParsesEnumsAndSources()
    {
        const string json = """
            {"sources":[{"id":"s","title":"t","retrieved":"2026-01-01","confidence":"documented"}],
             "banks":[{"id":"x","name":"X Bank","code":"99","type":"mortgageFinance","status":"merged",
                       "pesalink":"yes","sources":["s"]}]}
            """;
        var dir = BankDirectory.FromJson(json);
        var bank = Assert.Single(dir.All);
        Assert.Equal(BankType.MortgageFinance, bank.Type);
        Assert.Equal(BankStatus.Merged, bank.Status);
        Assert.Single(dir.PesalinkParticipants());
    }

    [GeneratedRegex("^[0-9]{2}$")]
    private static partial Regex MyRegex();
}
