using FnBManagement.Web.Data;
using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Tests.Data.Repositories;

public class InventoryRepositoryTests
{
    [Fact]
    public async Task ListLowStockAsync_ReturnsOnlyItemsAtOrBelowReorderLevel()
    {
        var options = new DbContextOptionsBuilder<FnBManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        await using var context = new FnBManagementDbContext(options);
        context.InventoryItems.AddRange(
            new InventoryItem { Name = "Milk", Unit = "units", QuantityInStock = 2, ReorderLevel = 5 },
            new InventoryItem { Name = "Coffee Beans", Unit = "units", QuantityInStock = 10, ReorderLevel = 5 });
        await context.SaveChangesAsync();

        var alerts = await new InventoryRepository(context).ListLowStockAsync();

        var milk = Assert.Single(alerts);
        Assert.Equal("Milk", milk.Name);
        Assert.Equal(2, milk.QuantityInStock);
        Assert.Equal(5, milk.ReorderLevel);
    }
}
