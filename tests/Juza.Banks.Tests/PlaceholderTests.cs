using Xunit;

namespace Juza.Banks.Tests;

public class PlaceholderTests
{
    [Fact]
    public void NameIsJuzaBanks()
    {
        Assert.Equal("Juza.Banks", Placeholder.Name);
    }
}
