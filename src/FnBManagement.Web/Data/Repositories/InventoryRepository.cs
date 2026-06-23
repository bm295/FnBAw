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

    public async Task<IReadOnlyList<InventoryItem>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.InventoryItems
            .AsNoTracking()
            .OrderBy(inventoryItem => inventoryItem.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> ListLowStockAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.InventoryItems
            .AsNoTracking()
            .Where(inventoryItem => inventoryItem.QuantityInStock <= inventoryItem.ReorderLevel)
            .OrderBy(inventoryItem => inventoryItem.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<InventoryItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.InventoryItems
            .AsNoTracking()
            .FirstOrDefaultAsync(inventoryItem => inventoryItem.Id == id, cancellationToken);
    }

    public async Task AddAsync(InventoryItem inventoryItem, CancellationToken cancellationToken = default)
    {
        _dbContext.InventoryItems.Add(inventoryItem);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(InventoryItem inventoryItem, CancellationToken cancellationToken = default)
    {
        var existingInventoryItem = await _dbContext.InventoryItems
            .FirstOrDefaultAsync(existingItem => existingItem.Id == inventoryItem.Id, cancellationToken);

        if (existingInventoryItem is null)
        {
            return false;
        }

        _dbContext.Entry(existingInventoryItem).CurrentValues.SetValues(inventoryItem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> AdjustStockAsync(int id, decimal adjustment, CancellationToken cancellationToken = default)
    {
        var inventoryItem = await _dbContext.InventoryItems
            .FirstOrDefaultAsync(existingItem => existingItem.Id == id, cancellationToken);

        if (inventoryItem is null)
        {
            return false;
        }

        inventoryItem.QuantityInStock = Math.Max(0, inventoryItem.QuantityInStock + adjustment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
