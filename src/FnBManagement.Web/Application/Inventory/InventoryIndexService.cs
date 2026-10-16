using FnBManagement.Web.Application.Abstractions;
using FnBManagement.Web.Application.Search;
using FnBManagement.Web.Domain.Entities;

namespace FnBManagement.Web.Application.Inventory;

public class InventoryIndexService(IInventoryRepository inventoryRepository) : IInventoryIndexService
{
    public async Task<IReadOnlyList<InventoryItem>> SearchAsync(string? searchTerm, bool lowStockOnly = false, CancellationToken cancellationToken = default)
    {
        var searchTerms = SearchTermParser.Parse(searchTerm);
        var items = await inventoryRepository.ListAsync(cancellationToken);

        return items.Where(item =>
            searchTerms.All(term => item.Name.Contains(term, StringComparison.OrdinalIgnoreCase)) &&
            (!lowStockOnly || item.IsLowStock)).ToList();
    }
}
