using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Controllers;

[ApiController]
[Route("api/inventory")]
public class InventoryController(IInventoryRepository inventoryRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, bool lowStockOnly = false, CancellationToken cancellationToken = default)
    {
        var items = await inventoryRepository.ListAsync(cancellationToken);
        return Ok(items.Where(item =>
            (string.IsNullOrWhiteSpace(searchTerm) || item.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) &&
            (!lowStockOnly || item.IsLowStock)).ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await inventoryRepository.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> Reorder(CancellationToken cancellationToken) =>
        Ok(await inventoryRepository.ListLowStockAsync(cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InventoryItem item, CancellationToken cancellationToken)
    {
        await inventoryRepository.AddAsync(item, cancellationToken);
        return CreatedAtAction(nameof(Details), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int id, [FromBody] InventoryItem item, CancellationToken cancellationToken)
    {
        if (id != item.Id) return BadRequest();
        return await inventoryRepository.UpdateAsync(item, cancellationToken) ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/adjust")]
    public async Task<IActionResult> AdjustStock(int id, [FromBody] StockAdjustment request, CancellationToken cancellationToken) =>
        await inventoryRepository.AdjustStockAsync(id, request.Quantity, cancellationToken) ? NoContent() : NotFound();
}

public record StockAdjustment(decimal Quantity);
