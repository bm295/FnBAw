using System.ComponentModel.DataAnnotations;

namespace FnBManagement.Web.Models;

public class Supplier
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Supplier name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Supplier name must be between 2 and 160 characters.")]
    public string Name { get; init; } = string.Empty;

    [StringLength(160, ErrorMessage = "Contact name cannot exceed 160 characters.")]
    public string? ContactName { get; init; }

    [EmailAddress(ErrorMessage = "Supplier email must be a valid email address.")]
    [StringLength(254, ErrorMessage = "Supplier email cannot exceed 254 characters.")]
    public string? Email { get; init; }

    [Phone(ErrorMessage = "Supplier phone must be a valid phone number.")]
    [StringLength(40, ErrorMessage = "Supplier phone cannot exceed 40 characters.")]
    public string? Phone { get; init; }

    [StringLength(240, ErrorMessage = "Supplier address cannot exceed 240 characters.")]
    public string? Address { get; init; }

    [StringLength(500, ErrorMessage = "Supplier notes cannot exceed 500 characters.")]
    public string? Notes { get; init; }

    public bool IsActive { get; set; } = true;
}
