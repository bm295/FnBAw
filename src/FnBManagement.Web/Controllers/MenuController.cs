using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using FnBManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Controllers;

[ApiController]
[Route("api/menu")]
public class MenuController(IMenuRepository menuRepository, IMenuIndexService menuIndexService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, string? category, CancellationToken cancellationToken) =>
        Ok(await menuIndexService.BuildIndexAsync(searchTerm, category, cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await menuRepository.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MenuItem item, CancellationToken cancellationToken)
    {
        await menuRepository.AddAsync(item, cancellationToken);
        return CreatedAtAction(nameof(Details), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int id, [FromBody] MenuItem item, CancellationToken cancellationToken)
    {
        if (id != item.Id) return BadRequest();
        return await menuRepository.UpdateAsync(item, cancellationToken) ? NoContent() : NotFound();
    }

    [HttpPost("{id:int}/archive")]
    public async Task<IActionResult> Archive(int id, CancellationToken cancellationToken) =>
        await menuRepository.ArchiveAsync(id, cancellationToken) ? NoContent() : NotFound();
}
