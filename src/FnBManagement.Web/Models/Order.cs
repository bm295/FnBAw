using System.ComponentModel.DataAnnotations;

namespace FnBManagement.Web.Models;

public class Order
{
    public int Id { get; init; }

    [Display(Name = "Order time")]
    [Required(ErrorMessage = "Order time is required.")]
    public DateTime OrderedAtUtc { get; init; }

    [Display(Name = "Ordered items")]
    [Required(ErrorMessage = "At least one ordered item is required.")]
    [MinLength(1, ErrorMessage = "At least one ordered item is required.")]
    public List<OrderLineItem> Lines { get; init; } = [];

    [Display(Name = "Order total")]
    [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "Order total must be greater than zero.")]
    public decimal Total => Lines.Sum(x => x.Quantity * x.UnitPrice);
}
