namespace FnBManagement.Web.Options;

public class ApplicationOptions
{
    public const string SectionName = "Application";

    public string ProductName { get; set; } = "FnB Management";

    public string SupportEmail { get; set; } = "support@example.com";

    public string Currency { get; set; } = "USD";

    public string Timezone { get; set; } = "UTC";
}
