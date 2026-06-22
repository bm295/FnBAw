using FnBManagement.Web.Models;

namespace FnBManagement.Web.Services;

public interface IDashboardService
{
    Task<DashboardViewModel> BuildDashboardAsync(CancellationToken cancellationToken = default);
}
