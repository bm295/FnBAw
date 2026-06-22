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
}
