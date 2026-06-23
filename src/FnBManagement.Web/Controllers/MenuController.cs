using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Controllers;

public class MenuController : Controller
{
    private readonly IMenuRepository _menuRepository;

    public MenuController(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<IActionResult> Index(string? searchTerm, string? category, CancellationToken cancellationToken)
    {
        var menuItems = await _menuRepository.ListAsync(cancellationToken);
        var categories = menuItems
            .Select(menuItem => menuItem.Category)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(menuCategory => menuCategory)
            .ToList();

        var filteredItems = menuItems.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filteredItems = filteredItems.Where(menuItem =>
                menuItem.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            filteredItems = filteredItems.Where(menuItem =>
                string.Equals(menuItem.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        var viewModel = new MenuIndexViewModel
        {
            MenuItems = filteredItems.ToList(),
            Categories = categories,
            SearchTerm = searchTerm,
            Category = category
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var menuItem = await _menuRepository.GetByIdAsync(id, cancellationToken);

        return menuItem is null ? NotFound() : View(menuItem);
    }

    public IActionResult Create()
    {
        return View(new MenuItem());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MenuItem menuItem, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(menuItem);
        }

        await _menuRepository.AddAsync(menuItem, cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var menuItem = await _menuRepository.GetByIdAsync(id, cancellationToken);

        return menuItem is null ? NotFound() : View(menuItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MenuItem menuItem, CancellationToken cancellationToken)
    {
        if (id != menuItem.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(menuItem);
        }

        var updated = await _menuRepository.UpdateAsync(menuItem, cancellationToken);

        return updated ? RedirectToAction(nameof(Index)) : NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Archive(int id, CancellationToken cancellationToken)
    {
        var archived = await _menuRepository.ArchiveAsync(id, cancellationToken);

        return archived ? RedirectToAction(nameof(Index)) : NotFound();
    }
}
