# Environment Variables

The FnB Management web application uses ASP.NET Core configuration, so environment variables override matching `appsettings.json` values. Use double underscores (`__`) to represent nested configuration keys.

## Required in production

| Environment variable | Configuration key | Description | Example |
| --- | --- | --- | --- |
| `ConnectionStrings__DefaultConnection` | `ConnectionStrings:DefaultConnection` | Database connection string used by the application. Store this in the deployment secret manager rather than source control. | `Host=db.example.com;Port=5432;Database=fnb_management;Username=fnb_app;Password=<secret>` |
| `AllowedHosts` | `AllowedHosts` | Semicolon-delimited host names the ASP.NET Core host is allowed to serve. Avoid `*` in production. | `app.example.com;www.example.com` |

## Application settings

| Environment variable | Configuration key | Description | Default |
| --- | --- | --- | --- |
| `Application__ProductName` | `Application:ProductName` | Public product name shown in operational messages and future UI surfaces. | `FnB Management` |
| `Application__SupportEmail` | `Application:SupportEmail` | Customer support contact email for production incidents and user help. | `support@example.com` |
| `Application__Currency` | `Application:Currency` | ISO 4217 currency code used for prices, revenue, and reports. | `USD` |
| `Application__Timezone` | `Application:Timezone` | IANA timezone used for business-day calculations and reports. | `UTC` |

## Logging settings

| Environment variable | Configuration key | Description | Production default |
| --- | --- | --- | --- |
| `Logging__LogLevel__Default` | `Logging:LogLevel:Default` | Minimum log level for application logs. | `Warning` |
| `Logging__LogLevel__Microsoft.AspNetCore` | `Logging:LogLevel:Microsoft.AspNetCore` | Minimum log level for ASP.NET Core framework logs. | `Warning` |
| `Logging__LogLevel__Microsoft.Hosting.Lifetime` | `Logging:LogLevel:Microsoft.Hosting.Lifetime` | Minimum log level for host lifecycle logs. | `Information` |

## Local development example

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=fnb_management;Username=fnb_user;Password=change-me"
export Application__SupportEmail="support@localhost"
export Application__Currency="USD"
export Application__Timezone="America/New_York"
```
