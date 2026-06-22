using FnBManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly IDashboardService _dashboardService;

    public HomeController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var viewModel = await _dashboardService.BuildDashboardAsync(cancellationToken);
        return View(viewModel);
    }
}
