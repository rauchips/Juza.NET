using Xunit;

namespace Juza.Paybills.Tests;

public class PlaceholderTests
{
    [Fact]
    public void NameIsJuzaPaybills()
    {
        Assert.Equal("Juza.Paybills", Placeholder.Name);
    }
}
