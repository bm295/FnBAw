using Microsoft.Extensions.Configuration;

namespace FnBManagement.Web.Configuration;

public static class ConnectionStringGuard
{
    public static string GetRequiredDefaultConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is required.");
        }

        return connectionString;
    }
}
