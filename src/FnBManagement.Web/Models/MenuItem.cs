using System.ComponentModel.DataAnnotations;

namespace FnBManagement.Web.Models;

public class MenuItem
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Menu item name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Menu item name must be between 2 and 160 characters.")]
    public string Name { get; init; } = string.Empty;

    [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; init; }

    [Required(ErrorMessage = "Menu item category is required.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Menu item category must be between 2 and 80 characters.")]
    public string Category { get; init; } = string.Empty;

    [Display(Name = "Available")]
    [Required(ErrorMessage = "Availability is required.")]
    public bool IsAvailable { get; set; } = true;
}
