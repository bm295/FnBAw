using FnBManagement.Web.Options;

namespace FnBManagement.Web.Tests.Options;

public class ApplicationOptionsTests
{
    [Fact]
    public void Defaults_AreConfigured()
    {
        var options = new ApplicationOptions();

        Assert.Equal("Application", ApplicationOptions.SectionName);
        Assert.Equal("FnB Management", options.ProductName);
        Assert.Equal("support@example.com", options.SupportEmail);
        Assert.Equal("USD", options.Currency);
        Assert.Equal("UTC", options.Timezone);
    }
}
