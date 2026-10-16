using FnBManagement.Web.Application.Abstractions;
using FnBManagement.Web.Application.Dashboard;
using FnBManagement.Web.Application.Inventory;
using FnBManagement.Web.Application.Menu;
using FnBManagement.Web.Infrastructure.Persistence;
using FnBManagement.Web.Infrastructure.Persistence.Repositories;
using FnBManagement.Web.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.SectionName));
builder.Services.AddDbContext<FnBManagementDbContext>(options =>
    options.UseNpgsql(FnBManagement.Web.Configuration.ConnectionStringGuard.GetRequiredDefaultConnectionString(builder.Configuration)));
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuIndexService, MenuIndexService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryIndexService, InventoryIndexService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await SeedData.InitializeAsync(app.Services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    ForwardLimit = 1,
    // Trust the front proxy/load balancer so AWS can pass the original scheme and client IP.
    KnownIPNetworks = { },
    KnownProxies = { }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
