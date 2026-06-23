using FnBManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Data;

public static class SeedData
{
    public static readonly IReadOnlyList<string> DemoRestaurants = new List<string>
    {
        "Harbor & Hearth Bistro",
        "Sunrise Market Cafe"
    };

    public static async Task InitializeAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FnBManagementDbContext>();

        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.MenuItems.AnyAsync(cancellationToken))
        {
            return;
        }

        var menuItems = CreateMenuItems();
        var inventoryItems = CreateInventoryItems();

        dbContext.MenuItems.AddRange(menuItems);
        dbContext.InventoryItems.AddRange(inventoryItems);
        await dbContext.SaveChangesAsync(cancellationToken);

        dbContext.Orders.AddRange(CreateOrders(menuItems));
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static List<MenuItem> CreateMenuItems()
    {
        return new List<MenuItem>
        {
            new()
            {
                Name = "Harbor Chowder Bowl",
                Category = "Harbor & Hearth Bistro - Soups",
                Price = 12.50m,
                IsAvailable = true
            },
            new()
            {
                Name = "Smoked Brisket Sandwich",
                Category = "Harbor & Hearth Bistro - Mains",
                Price = 16.75m,
                IsAvailable = true
            },
            new()
            {
                Name = "Roasted Vegetable Grain Bowl",
                Category = "Sunrise Market Cafe - Bowls",
                Price = 13.25m,
                IsAvailable = true
            },
            new()
            {
                Name = "Citrus Iced Tea",
                Category = "Sunrise Market Cafe - Beverages",
                Price = 4.50m,
                IsAvailable = true
            },
            new()
            {
                Name = "Seasonal Berry Tart",
                Category = "Sunrise Market Cafe - Desserts",
                Price = 7.00m,
                IsAvailable = false
            }
        };
    }

    private static List<InventoryItem> CreateInventoryItems()
    {
        return new List<InventoryItem>
        {
            new()
            {
                Name = "Clam Stock",
                Unit = "liters",
                QuantityInStock = 18m,
                ReorderLevel = 10m
            },
            new()
            {
                Name = "Smoked Brisket",
                Unit = "pounds",
                QuantityInStock = 9m,
                ReorderLevel = 12m
            },
            new()
            {
                Name = "Ancient Grain Mix",
                Unit = "pounds",
                QuantityInStock = 32m,
                ReorderLevel = 15m
            },
            new()
            {
                Name = "Citrus Tea Concentrate",
                Unit = "liters",
                QuantityInStock = 6m,
                ReorderLevel = 8m
            },
            new()
            {
                Name = "Fresh Berries",
                Unit = "pints",
                QuantityInStock = 4m,
                ReorderLevel = 6m
            }
        };
    }

    private static List<Order> CreateOrders(IReadOnlyList<MenuItem> menuItems)
    {
        var menuByName = menuItems.ToDictionary(menuItem => menuItem.Name);
        var today = DateTime.UtcNow.Date;

        return new List<Order>
        {
            new()
            {
                OrderedAtUtc = today.AddHours(11).AddMinutes(35),
                Lines = new List<OrderLineItem>
                {
                    CreateOrderLine(menuByName["Harbor Chowder Bowl"], 2),
                    CreateOrderLine(menuByName["Citrus Iced Tea"], 2)
                }
            },
            new()
            {
                OrderedAtUtc = today.AddHours(12).AddMinutes(10),
                Lines = new List<OrderLineItem>
                {
                    CreateOrderLine(menuByName["Smoked Brisket Sandwich"], 1),
                    CreateOrderLine(menuByName["Roasted Vegetable Grain Bowl"], 1),
                    CreateOrderLine(menuByName["Citrus Iced Tea"], 1)
                }
            },
            new()
            {
                OrderedAtUtc = today.AddDays(-1).AddHours(18).AddMinutes(20),
                Lines = new List<OrderLineItem>
                {
                    CreateOrderLine(menuByName["Roasted Vegetable Grain Bowl"], 3),
                    CreateOrderLine(menuByName["Seasonal Berry Tart"], 2)
                }
            }
        };
    }

    private static OrderLineItem CreateOrderLine(MenuItem menuItem, int quantity)
    {
        return new OrderLineItem
        {
            MenuItemId = menuItem.Id,
            MenuItemName = menuItem.Name,
            Quantity = quantity,
            UnitPrice = menuItem.Price
        };
    }
}
