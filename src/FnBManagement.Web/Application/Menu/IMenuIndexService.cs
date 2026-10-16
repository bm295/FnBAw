
namespace FnBManagement.Web.Application.Menu;

public interface IMenuIndexService
{
    Task<MenuIndexViewModel> BuildIndexAsync(string? searchTerm, string? category, CancellationToken cancellationToken = default);
}
