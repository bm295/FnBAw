using FnBManagement.Web.Application.Abstractions;
using FnBManagement.Web.Application.Search;

namespace FnBManagement.Web.Application.Menu;

public class MenuIndexService : IMenuIndexService
{
    private readonly IMenuRepository _menuRepository;

    public MenuIndexService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<MenuIndexViewModel> BuildIndexAsync(string? searchTerm, string? category, CancellationToken cancellationToken = default)
    {
        searchTerm = searchTerm?.Trim();
        var searchTerms = SearchTermParser.Parse(searchTerm);

        var menuItems = await _menuRepository.ListAsync(cancellationToken);
        var availableItems = menuItems.Where(menuItem => menuItem.IsAvailable);
        var categories = availableItems
            .Select(menuItem => menuItem.Category)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(menuCategory => menuCategory)
            .ToList();

        var filteredItems = availableItems;

        if (searchTerms.Count > 0)
        {
            filteredItems = filteredItems.Where(menuItem =>
                searchTerms.All(term => menuItem.Name.Contains(term, StringComparison.OrdinalIgnoreCase)));
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
