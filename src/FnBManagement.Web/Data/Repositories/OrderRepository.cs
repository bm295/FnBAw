using FnBManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly FnBManagementDbContext _dbContext;

    public OrderRepository(FnBManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Order>> ListForDateAsync(DateTime utcDate, CancellationToken cancellationToken = default)
    {
        var startUtc = utcDate.Date;
        var endUtc = startUtc.AddDays(1);

        return await _dbContext.Orders
            .AsNoTracking()
            .Include(order => order.Lines)
            .Where(order => order.OrderedAtUtc >= startUtc && order.OrderedAtUtc < endUtc)
            .OrderByDescending(order => order.OrderedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> ListRecentAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Include(order => order.Lines)
            .OrderByDescending(order => order.OrderedAtUtc)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
