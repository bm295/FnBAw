using FnBManagement.Web.Models;

namespace FnBManagement.Web.Services;

public interface IMenuIndexService
{
    Task<MenuIndexViewModel> BuildIndexAsync(string? searchTerm, string? category, CancellationToken cancellationToken = default);
}
