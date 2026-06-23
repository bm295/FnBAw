using FnBManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Data.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly FnBManagementDbContext _dbContext;

    public MenuRepository(FnBManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.MenuItems.CountAsync(cancellationToken);
    }

    public Task<int> CountAvailableAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.MenuItems.CountAsync(menuItem => menuItem.IsAvailable, cancellationToken);
    }

    public async Task<IReadOnlyList<MenuItem>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.MenuItems
            .AsNoTracking()
            .OrderBy(menuItem => menuItem.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.MenuItems
            .AsNoTracking()
            .FirstOrDefaultAsync(menuItem => menuItem.Id == id, cancellationToken);
    }

    public async Task AddAsync(MenuItem menuItem, CancellationToken cancellationToken = default)
    {
        _dbContext.MenuItems.Add(menuItem);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(MenuItem menuItem, CancellationToken cancellationToken = default)
    {
        if (!await _dbContext.MenuItems.AnyAsync(existingMenuItem => existingMenuItem.Id == menuItem.Id, cancellationToken))
        {
            return false;
        }

        _dbContext.MenuItems.Update(menuItem);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ArchiveAsync(int id, CancellationToken cancellationToken = default)
    {
        var menuItem = await _dbContext.MenuItems.FindAsync(new object[] { id }, cancellationToken);

        if (menuItem is null)
        {
            return false;
        }

        menuItem.IsAvailable = false;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
