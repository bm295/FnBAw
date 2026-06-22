using FnBManagement.Web.Models;

namespace FnBManagement.Web.Data.Repositories;

public interface IMenuRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAvailableAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MenuItem>> ListAsync(CancellationToken cancellationToken = default);
}
