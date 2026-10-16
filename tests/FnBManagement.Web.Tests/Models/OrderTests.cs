using FnBManagement.Web.Domain.Entities;

namespace FnBManagement.Web.Tests.Models;

public class OrderTests
{
    [Fact]
    public void Total_SumsLineQuantitiesAndPrices()
    {
        var order = new Order
        {
            Lines =
            [
                new OrderLineItem { Quantity = 2, UnitPrice = 12.5m },
                new OrderLineItem { Quantity = 3, UnitPrice = 4m }
            ]
        };

        Assert.Equal(37m, order.Total);
    }
}
