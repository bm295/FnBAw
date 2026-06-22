using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;

namespace FnBManagement.Web.Services;

public class DashboardService : IDashboardService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IOrderRepository _orderRepository;

    public DashboardService(
        IMenuRepository menuRepository,
        IInventoryRepository inventoryRepository,
        IOrderRepository orderRepository)
    {
        _menuRepository = menuRepository;
        _inventoryRepository = inventoryRepository;
        _orderRepository = orderRepository;
    }

    public async Task<DashboardViewModel> BuildDashboardAsync(CancellationToken cancellationToken = default)
    {
        var utcToday = DateTime.UtcNow.Date;
        var ordersToday = await _orderRepository.ListForDateAsync(utcToday, cancellationToken);
        var lowStockItems = await _inventoryRepository.ListLowStockAsync(cancellationToken);

        return new DashboardViewModel
        {
            MenuItemsCount = await _menuRepository.CountAsync(cancellationToken),
            AvailableMenuItemsCount = await _menuRepository.CountAvailableAsync(cancellationToken),
            InventoryItemsCount = await _inventoryRepository.CountAsync(cancellationToken),
            LowStockItemsCount = lowStockItems.Count,
            OrdersTodayCount = ordersToday.Count,
            RevenueToday = ordersToday.Sum(order => order.Total),
            LowStockItems = lowStockItems,
            RecentOrders = await _orderRepository.ListRecentAsync(5, cancellationToken)
        };
    }
}
