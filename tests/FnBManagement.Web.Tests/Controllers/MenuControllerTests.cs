using FnBManagement.Web.Controllers;
using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Models;
using FnBManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FnBManagement.Web.Tests.Controllers;

public class MenuControllerTests
{
    [Fact]
    public async Task Index_ReturnsViewModelFromMenuIndexService()
    {
        var viewModel = new MenuIndexViewModel
        {
            MenuItems =
            [
                new MenuItem { Id = 1, Name = "Burger", Category = "Mains", Price = 10, IsAvailable = true }
            ],
            Categories =
            [
                "Mains"
            ],
            SearchTerm = "burger",
            Category = "Mains"
        };

        var controller = new MenuController(new FakeMenuRepository(), new FakeMenuIndexService(viewModel));

        var result = await controller.Index("burger", "Mains", CancellationToken.None);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(viewModel, viewResult.Model);
    }

    private sealed class FakeMenuRepository : IMenuRepository
    {
        public Task<int> CountAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);

        public Task<int> CountAvailableAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);

        public Task<IReadOnlyList<MenuItem>> ListAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<MenuItem>>([]);

        public Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult<MenuItem?>(null);

        public Task AddAsync(MenuItem menuItem, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<bool> UpdateAsync(MenuItem menuItem, CancellationToken cancellationToken = default) => Task.FromResult(false);

        public Task<bool> ArchiveAsync(int id, CancellationToken cancellationToken = default) => Task.FromResult(false);
    }

    private sealed class FakeMenuIndexService : IMenuIndexService
    {
        private readonly MenuIndexViewModel _viewModel;

        public FakeMenuIndexService(MenuIndexViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Task<MenuIndexViewModel> BuildIndexAsync(string? searchTerm, string? category, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_viewModel);
        }
    }
}
