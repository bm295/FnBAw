using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Controllers;

public class InventoryController : Controller
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryController(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<IActionResult> Index(string? searchTerm, bool lowStockOnly = false, CancellationToken cancellationToken = default)
    {
        var inventoryItems = await _inventoryRepository.ListAsync(cancellationToken);
        var filteredItems = inventoryItems.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filteredItems = filteredItems.Where(inventoryItem =>
                inventoryItem.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (lowStockOnly)
        {
            filteredItems = filteredItems.Where(inventoryItem => inventoryItem.IsLowStock);
        }

        ViewData["SearchTerm"] = searchTerm;
        ViewData["LowStockOnly"] = lowStockOnly;

        return View(filteredItems.ToList());
    }

    public IActionResult Create()
    {
        return View(new InventoryItem());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(inventoryItem);
        }

        await _inventoryRepository.AddAsync(inventoryItem, cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var inventoryItem = await _inventoryRepository.GetByIdAsync(id, cancellationToken);

        return inventoryItem is null ? NotFound() : View(inventoryItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        if (id != inventoryItem.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(inventoryItem);
        }

        var updated = await _inventoryRepository.UpdateAsync(inventoryItem, cancellationToken);

        return updated ? RedirectToAction(nameof(Index)) : NotFound();
    }

    public async Task<IActionResult> AdjustStock(int id, CancellationToken cancellationToken)
    {
        var inventoryItem = await _inventoryRepository.GetByIdAsync(id, cancellationToken);

        return inventoryItem is null ? NotFound() : View(inventoryItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdjustStock(int id, decimal adjustment, CancellationToken cancellationToken)
    {
        var adjusted = await _inventoryRepository.AdjustStockAsync(id, adjustment, cancellationToken);

        if (!adjusted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Reorder(CancellationToken cancellationToken)
    {
        var lowStockItems = await _inventoryRepository.ListLowStockAsync(cancellationToken);

        return View(lowStockItems);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder(int id, decimal quantity, CancellationToken cancellationToken)
    {
        if (quantity <= 0)
        {
            ModelState.AddModelError(nameof(quantity), "Reorder quantity must be greater than zero.");
            var lowStockItems = await _inventoryRepository.ListLowStockAsync(cancellationToken);

            return View(lowStockItems);
        }

        var reordered = await _inventoryRepository.AdjustStockAsync(id, quantity, cancellationToken);

        return reordered ? RedirectToAction(nameof(Index)) : NotFound();
    }
}
