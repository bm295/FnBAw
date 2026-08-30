using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using FnBManagement.Web.Services;

namespace FnBManagement.Web.Tests.Services;

public class DashboardServiceTests
{
    [Fact]
    public async Task BuildDashboardAsync_ComputesCountsAndRevenue()
    {
        var today = DateTime.UtcNow.Date;
        var menuRepository = new FakeMenuRepository(10, 7);
        var inventoryRepository = new FakeInventoryRepository(
            4,
            [
                new InventoryItem { Id = 1, Name = "Rice", Unit = "kg", QuantityInStock = 2, ReorderLevel = 5 },
                new InventoryItem { Id = 2, Name = "Oil", Unit = "l", QuantityInStock = 10, ReorderLevel = 3 }
            ]);
        var orderRepository = new FakeOrderRepository(
            [
                new Order
                {
                    Id = 1,
                    OrderedAtUtc = today.AddHours(1),
                    Lines =
                    [
                        new OrderLineItem { Quantity = 2, UnitPrice = 12.50m }
                    ]
                },
                new Order
                {
                    Id = 2,
                    OrderedAtUtc = today.AddHours(3),
                    Lines =
                    [
                        new OrderLineItem { Quantity = 1, UnitPrice = 5.00m }
                    ]
                }
            ]);

        var service = new DashboardService(menuRepository, inventoryRepository, orderRepository);

        var dashboard = await service.BuildDashboardAsync();

        Assert.Equal(10, dashboard.MenuItemsCount);
        Assert.Equal(7, dashboard.AvailableMenuItemsCount);
        Assert.Equal(4, dashboard.InventoryItemsCount);
        Assert.Equal(2, dashboard.LowStockItemsCount);
        Assert.Equal(2, dashboard.OrdersTodayCount);
        Assert.Equal(30.00m, dashboard.RevenueToday);
        Assert.Equal(2, dashboard.LowStockItems.Count);
        Assert.Equal(2, dashboard.RecentOrders.Count);
    }

    private sealed class FakeMenuRepository : IMenuRepository
    {
        private readonly int _count;
        private readonly int _availableCount;

        public FakeMenuRepository(int count, int availableCount)
        {
            _count = count;
            _availableCount = availableCount;
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default) => Task.FromResult(_count);

        public Task<int> CountAvailableAsync(CancellationToken cancellationToken = default) => Task.FromResult(_availableCount);

        public Task<IReadOnlyList<MenuItem>> ListAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<MenuItem>>([]);

        public Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult<MenuItem?>(null);

        public Task AddAsync(MenuItem menuItem, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<bool> UpdateAsync(MenuItem menuItem, CancellationToken cancellationToken = default) => Task.FromResult(false);

        public Task<bool> ArchiveAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult(false);
    }

    private sealed class FakeInventoryRepository : IInventoryRepository
    {
        private readonly int _count;
        private readonly IReadOnlyList<InventoryItem> _lowStockItems;

        public FakeInventoryRepository(int count, IReadOnlyList<InventoryItem> lowStockItems)
        {
            _count = count;
            _lowStockItems = lowStockItems;
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default) => Task.FromResult(_count);

        public Task<IReadOnlyList<InventoryItem>> ListAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<InventoryItem>>([]);

        public Task<IReadOnlyList<InventoryItem>> ListLowStockAsync(CancellationToken cancellationToken = default) => Task.FromResult(_lowStockItems);

        public Task<InventoryItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult<InventoryItem?>(null);

        public Task AddAsync(InventoryItem inventoryItem, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<bool> UpdateAsync(InventoryItem inventoryItem, CancellationToken cancellationToken = default) => Task.FromResult(false);

        public Task<bool> AdjustStockAsync(int id, decimal adjustment, CancellationToken cancellationToken = default) => Task.FromResult(false);
    }

    private sealed class FakeOrderRepository : IOrderRepository
    {
        private readonly IReadOnlyList<Order> _orders;

        public FakeOrderRepository(IReadOnlyList<Order> orders)
        {
            _orders = orders;
        }

        public Task<IReadOnlyList<Order>> ListForDateAsync(DateTime utcDate, CancellationToken cancellationToken = default) => Task.FromResult(_orders);

        public Task<IReadOnlyList<Order>> ListRecentAsync(int count, CancellationToken cancellationToken = default) => Task.FromResult(_orders);
    }
}
