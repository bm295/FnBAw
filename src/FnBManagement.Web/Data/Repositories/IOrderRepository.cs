using FnBManagement.Web.Models;

namespace FnBManagement.Web.Data.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> ListForDateAsync(DateTime utcDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> ListRecentAsync(int count, CancellationToken cancellationToken = default);
}
