using FnBManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Controllers;

[ApiController]
[Route("api/dashboard")]
public class HomeController(IDashboardService dashboardService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
        Ok(await dashboardService.BuildDashboardAsync(cancellationToken));
}
