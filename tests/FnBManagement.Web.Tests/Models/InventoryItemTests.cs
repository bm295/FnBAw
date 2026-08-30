using FnBManagement.Web.Models;

namespace FnBManagement.Web.Tests.Models;

public class InventoryItemTests
{
    [Fact]
    public void IsLowStock_ReturnsTrue_WhenQuantityIsAtOrBelowReorderLevel()
    {
        var item = new InventoryItem
        {
            Name = "Milk",
            Unit = "litre",
            QuantityInStock = 5,
            ReorderLevel = 5
        };

        Assert.True(item.IsLowStock);
    }
}
