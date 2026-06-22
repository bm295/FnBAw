using FnBManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Data.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly FnBManagementDbContext _dbContext;

    public InventoryRepository(FnBManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.InventoryItems.CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> ListLowStockAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.InventoryItems
            .AsNoTracking()
            .Where(inventoryItem => inventoryItem.QuantityInStock <= inventoryItem.ReorderLevel)
            .OrderBy(inventoryItem => inventoryItem.Name)
            .ToListAsync(cancellationToken);
    }
}
