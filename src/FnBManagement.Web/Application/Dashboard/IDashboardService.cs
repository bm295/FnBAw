
namespace FnBManagement.Web.Application.Dashboard;

public interface IDashboardService
{
    Task<DashboardViewModel> BuildDashboardAsync(CancellationToken cancellationToken = default);
}
