using System.ComponentModel.DataAnnotations;

namespace FnBManagement.Web.Models;

public class OrderLineItem
{
    public int MenuItemId { get; init; }

    [Required(ErrorMessage = "Ordered item name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Ordered item name must be between 2 and 160 characters.")]
    public string MenuItemName { get; init; } = string.Empty;

    [Range(1, 999, ErrorMessage = "Ordered item quantity must be at least one.")]
    public int Quantity { get; init; }

    [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "Ordered item unit price must be greater than zero.")]
    public decimal UnitPrice { get; init; }
}
