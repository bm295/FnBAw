using FnBManagement.Web.Domain.Entities;

namespace FnBManagement.Web.Application.Inventory;

public interface IInventoryIndexService
{
    Task<IReadOnlyList<InventoryItem>> SearchAsync(string? searchTerm, bool lowStockOnly = false, CancellationToken cancellationToken = default);
}
