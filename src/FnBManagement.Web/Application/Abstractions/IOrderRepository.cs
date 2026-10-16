using FnBManagement.Web.Domain.Entities;

namespace FnBManagement.Web.Application.Abstractions;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> ListForDateAsync(DateTime utcDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> ListRecentAsync(int count, CancellationToken cancellationToken = default);
}
