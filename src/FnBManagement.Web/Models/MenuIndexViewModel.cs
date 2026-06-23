namespace FnBManagement.Web.Models;

public class MenuIndexViewModel
{
    public IReadOnlyList<MenuItem> MenuItems { get; init; } = [];
    public IReadOnlyList<string> Categories { get; init; } = [];
    public string? SearchTerm { get; init; }
    public string? Category { get; init; }
}
