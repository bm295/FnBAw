using FnBManagement.Web.Domain.Entities;

namespace FnBManagement.Web.Application.Abstractions;

public interface IInventoryRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InventoryItem>> ListAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InventoryItem>> ListLowStockAsync(CancellationToken cancellationToken = default);
    Task<InventoryItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(InventoryItem inventoryItem, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(InventoryItem inventoryItem, CancellationToken cancellationToken = default);
    Task<bool> AdjustStockAsync(int id, decimal adjustment, CancellationToken cancellationToken = default);
}
