using System.ComponentModel.DataAnnotations;

namespace FnBManagement.Web.Models;

public class InventoryItem
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Inventory item name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Inventory item name must be between 2 and 160 characters.")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Inventory item unit is required.")]
    [StringLength(40, MinimumLength = 1, ErrorMessage = "Inventory item unit must be between 1 and 40 characters.")]
    public string Unit { get; init; } = string.Empty;

    [Display(Name = "Quantity")]
    [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "Quantity must be zero or greater.")]
    public decimal QuantityInStock { get; set; }

    [Display(Name = "Reorder level")]
    [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "Reorder level must be zero or greater.")]
    public decimal ReorderLevel { get; init; }

    public bool IsLowStock => QuantityInStock <= ReorderLevel;
}
