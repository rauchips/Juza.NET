using Xunit;

namespace Juza.Core.Tests;

public class PlaceholderTests
{
    [Fact]
    public void NameIsJuzaCore()
    {
        Assert.Equal("Juza.Core", Placeholder.Name);
    }
}
