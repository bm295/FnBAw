using FnBManagement.Web.Data;
using FnBManagement.Web.Data.Repositories;
using FnBManagement.Web.Services;
using FnBManagement.Web.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(ApplicationOptions.SectionName));
builder.Services.AddDbContext<FnBManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await SeedData.InitializeAsync(app.Services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
