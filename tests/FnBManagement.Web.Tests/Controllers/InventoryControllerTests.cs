using FnBManagement.Web.Infrastructure.Persistence.Repositories;
using FnBManagement.Web.Application.Abstractions;
using FnBManagement.Web.Application.Inventory;
using FnBManagement.Web.Domain.Entities;
using FnBManagement.Web.Infrastructure.Persistence;
using FnBManagement.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Tests.Controllers;

public class InventoryControllerTests
{
    [Fact]
    public async Task Index_ForwardsParametersAndReturnsServiceResults()
    {
        var options = new DbContextOptionsBuilder<FnBManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        await using var context = new FnBManagementDbContext(options);
        var service = new FakeInventoryIndexService();
        var controller = new InventoryController(new InventoryRepository(context), service);
        using var cancellation = new CancellationTokenSource();
        const string searchTerm = " chicken \"spicy sauce\" ";

        var result = Assert.IsType<OkObjectResult>(
            await controller.Index(searchTerm, true, cancellation.Token));

        Assert.Same(service.Items, result.Value);
        Assert.Equal(searchTerm, service.SearchTerm);
        Assert.True(service.LowStockOnly);
        Assert.Equal(cancellation.Token, service.CancellationToken);
    }

    private sealed class FakeInventoryIndexService : IInventoryIndexService
    {
        public IReadOnlyList<InventoryItem> Items { get; } =
            [new InventoryItem { Name = "Chicken spicy sauce", Unit = "kg" }];
        public string? SearchTerm { get; private set; }
        public bool LowStockOnly { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public Task<IReadOnlyList<InventoryItem>> SearchAsync(string? searchTerm, bool lowStockOnly = false, CancellationToken cancellationToken = default)
        {
            SearchTerm = searchTerm;
            LowStockOnly = lowStockOnly;
            CancellationToken = cancellationToken;
            return Task.FromResult(Items);
        }
    }
}
