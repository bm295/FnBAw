using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using FnBManagement.Web.Services;

namespace FnBManagement.Web.Tests.Services;

public class MenuIndexServiceTests
{
    [Fact]
    public async Task BuildIndexAsync_FiltersUnavailableItemsAndAppliesSearchAndCategory()
    {
        var repository = new FakeMenuRepository(
            [
                new MenuItem { Id = 1, Name = "Chicken Burger", Category = "Mains", Price = 10, IsAvailable = true },
                new MenuItem { Id = 2, Name = "Veg Soup", Category = "Starters", Price = 5, IsAvailable = true },
                new MenuItem { Id = 3, Name = "Spicy Burger", Category = "Mains", Price = 11, IsAvailable = true },
                new MenuItem { Id = 4, Name = "Archived Burger", Category = "Mains", Price = 12, IsAvailable = false }
            ]);
        var service = new MenuIndexService(repository);

        var result = await service.BuildIndexAsync("burger", "MAINS", CancellationToken.None);

        Assert.Equal("burger", result.SearchTerm);
        Assert.Equal("MAINS", result.Category);
        Assert.Equal(2, result.MenuItems.Count);
        Assert.All(result.MenuItems, item => Assert.True(item.IsAvailable));
        Assert.All(result.MenuItems, item => Assert.Equal("Mains", item.Category));
        Assert.Equal(2, result.Categories.Count);
        Assert.Contains("Mains", result.Categories);
        Assert.Contains("Starters", result.Categories);
    }

    private sealed class FakeMenuRepository : IMenuRepository
    {
        private readonly IReadOnlyList<MenuItem> _items;

        public FakeMenuRepository(IReadOnlyList<MenuItem> items)
        {
            _items = items;
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default) => Task.FromResult(_items.Count);

        public Task<int> CountAvailableAsync(CancellationToken cancellationToken = default) => Task.FromResult(_items.Count(item => item.IsAvailable));

        public Task<IReadOnlyList<MenuItem>> ListAsync(CancellationToken cancellationToken = default) => Task.FromResult(_items);

        public Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult(_items.FirstOrDefault(item => item.Id == id));

        public Task AddAsync(MenuItem menuItem, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<bool> UpdateAsync(MenuItem menuItem, CancellationToken cancellationToken = default) => Task.FromResult(true);

        public Task<bool> ArchiveAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult(true);
    }
}
