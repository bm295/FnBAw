using System.ComponentModel.DataAnnotations;

namespace FnBManagement.Web.Models;

public class Restaurant
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Restaurant name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Restaurant name must be between 2 and 160 characters.")]
    public string Name { get; init; } = string.Empty;

    [StringLength(160, ErrorMessage = "Legal name cannot exceed 160 characters.")]
    public string? LegalName { get; init; }

    [Required(ErrorMessage = "Restaurant email is required.")]
    [EmailAddress(ErrorMessage = "Restaurant email must be a valid email address.")]
    [StringLength(254, ErrorMessage = "Restaurant email cannot exceed 254 characters.")]
    public string Email { get; init; } = string.Empty;

    [Phone(ErrorMessage = "Restaurant phone must be a valid phone number.")]
    [StringLength(40, ErrorMessage = "Restaurant phone cannot exceed 40 characters.")]
    public string? Phone { get; init; }

    [StringLength(240, ErrorMessage = "Street address cannot exceed 240 characters.")]
    public string? StreetAddress { get; init; }

    [StringLength(120, ErrorMessage = "City cannot exceed 120 characters.")]
    public string? City { get; init; }

    [StringLength(80, ErrorMessage = "State or region cannot exceed 80 characters.")]
    public string? StateOrRegion { get; init; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters.")]
    public string? PostalCode { get; init; }

    [StringLength(80, ErrorMessage = "Country cannot exceed 80 characters.")]
    public string? Country { get; init; }

    [Required(ErrorMessage = "Timezone is required.")]
    [StringLength(80, ErrorMessage = "Timezone cannot exceed 80 characters.")]
    public string TimeZone { get; init; } = "UTC";

    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a three-letter ISO code.")]
    public string Currency { get; init; } = "USD";

    public bool IsActive { get; set; } = true;
}
