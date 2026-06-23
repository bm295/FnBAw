using FnBManagement.Web.Models;

namespace FnBManagement.Web.Data.Repositories;

public interface IMenuRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAvailableAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MenuItem>> ListAsync(CancellationToken cancellationToken = default);
    Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(MenuItem menuItem, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(MenuItem menuItem, CancellationToken cancellationToken = default);
    Task<bool> ArchiveAsync(int id, CancellationToken cancellationToken = default);
}
