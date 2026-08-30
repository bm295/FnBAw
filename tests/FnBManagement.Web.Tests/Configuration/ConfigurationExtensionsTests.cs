using Microsoft.Extensions.Configuration;

namespace FnBManagement.Web.Tests.Configuration;

public class ConnectionStringGuardTests
{
    [Fact]
    public void GetRequiredDefaultConnectionString_ReturnsValue_WhenConfigured()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = "Host=localhost;Database=fnb"
            })
            .Build();

        var result = FnBManagement.Web.Configuration.ConnectionStringGuard.GetRequiredDefaultConnectionString(configuration);

        Assert.Equal("Host=localhost;Database=fnb", result);
    }

    [Fact]
    public void GetRequiredDefaultConnectionString_Throws_WhenMissing()
    {
        var configuration = new ConfigurationBuilder().Build();

        var exception = Assert.Throws<InvalidOperationException>(
            () => FnBManagement.Web.Configuration.ConnectionStringGuard.GetRequiredDefaultConnectionString(configuration));

        Assert.Contains("DefaultConnection", exception.Message);
    }
}
