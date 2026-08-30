using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;

namespace FnBManagement.Web.Services;

public class MenuIndexService : IMenuIndexService
{
    private readonly IMenuRepository _menuRepository;

    public MenuIndexService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<MenuIndexViewModel> BuildIndexAsync(string? searchTerm, string? category, CancellationToken cancellationToken = default)
    {
        var menuItems = await _menuRepository.ListAsync(cancellationToken);
        var availableItems = menuItems.Where(menuItem => menuItem.IsAvailable);
        var categories = availableItems
            .Select(menuItem => menuItem.Category)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(menuCategory => menuCategory)
            .ToList();

        var filteredItems = availableItems;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filteredItems = filteredItems.Where(menuItem =>
                menuItem.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            filteredItems = filteredItems.Where(menuItem =>
                string.Equals(menuItem.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        return new MenuIndexViewModel
        {
            MenuItems = filteredItems.ToList(),
            Categories = categories,
            SearchTerm = searchTerm,
            Category = category
        };
    }
}
