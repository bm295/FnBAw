using FnBManagement.Web.Models;

namespace FnBManagement.Web.Data.Repositories;

public interface IInventoryRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InventoryItem>> ListLowStockAsync(CancellationToken cancellationToken = default);
}
