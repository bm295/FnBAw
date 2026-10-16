using FnBManagement.Web.Infrastructure.Persistence.Repositories;
using FnBManagement.Web.Application.Abstractions;
using FnBManagement.Web.Application.Inventory;
using FnBManagement.Web.Domain.Entities;
using FnBManagement.Web.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Tests.Services;

public class InventoryIndexServiceTests
{
    [Theory]
    [InlineData(" CHICKEN \"spicy sauce\" ", false, 2)]
    [InlineData("sauce chicken spicy", false, 3)]
    [InlineData("chicken \"spicy sauce\"", true, 1)]
    [InlineData("chicken \"spicy sauce", false, 2)]
    [InlineData("chicken missing", false, 0)]
    [InlineData(null, true, 2)]
    [InlineData(" \t ", false, 3)]
    public async Task SearchAsync_MatchesTermsAndKeepsLowStockFilter(string? searchTerm, bool lowStockOnly, int expectedCount)
    {
        var options = new DbContextOptionsBuilder<FnBManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        await using var context = new FnBManagementDbContext(options);
        context.InventoryItems.AddRange(
            new InventoryItem { Name = "Chicken with spicy sauce", Unit = "kg", QuantityInStock = 1, ReorderLevel = 2 },
            new InventoryItem { Name = "Spicy chicken with sauce", Unit = "kg", QuantityInStock = 1, ReorderLevel = 2 },
            new InventoryItem { Name = "Chicken spicy sauce mix", Unit = "kg", QuantityInStock = 5, ReorderLevel = 2 });
        await context.SaveChangesAsync();
        var service = new InventoryIndexService(new InventoryRepository(context));

        var items = await service.SearchAsync(searchTerm, lowStockOnly);

        Assert.Equal(expectedCount, items.Count);
        if (lowStockOnly)
            Assert.All(items, item => Assert.True(item.IsLowStock));
    }
}
