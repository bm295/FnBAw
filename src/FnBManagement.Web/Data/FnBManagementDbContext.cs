using FnBManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FnBManagement.Web.Data;

public class FnBManagementDbContext : DbContext
{
    public FnBManagementDbContext(DbContextOptions<FnBManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems");
            entity.HasKey(menuItem => menuItem.Id);
            entity.Property(menuItem => menuItem.Id).ValueGeneratedOnAdd();
            entity.Property(menuItem => menuItem.Name).HasMaxLength(160).IsRequired();
            entity.Property(menuItem => menuItem.Category).HasMaxLength(80).IsRequired();
            entity.Property(menuItem => menuItem.Price).HasPrecision(10, 2);
            entity.Property(menuItem => menuItem.IsAvailable).HasDefaultValue(true);
        });

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.ToTable("InventoryItems");
            entity.HasKey(inventoryItem => inventoryItem.Id);
            entity.Property(inventoryItem => inventoryItem.Id).ValueGeneratedOnAdd();
            entity.Property(inventoryItem => inventoryItem.Name).HasMaxLength(160).IsRequired();
            entity.Property(inventoryItem => inventoryItem.Unit).HasMaxLength(40).IsRequired();
            entity.Property(inventoryItem => inventoryItem.QuantityInStock).HasPrecision(10, 2);
            entity.Property(inventoryItem => inventoryItem.ReorderLevel).HasPrecision(10, 2);
            entity.Ignore(inventoryItem => inventoryItem.IsLowStock);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(order => order.Id);
            entity.Property(order => order.Id).ValueGeneratedOnAdd();
            entity.Property(order => order.OrderedAtUtc).IsRequired();
            entity.Ignore(order => order.Total);

            entity.OwnsMany(order => order.Lines, line =>
            {
                line.ToTable("OrderLines");
                line.WithOwner().HasForeignKey("OrderId");
                line.Property<int>("Id").ValueGeneratedOnAdd();
                line.HasKey("Id");
                line.Property(orderLine => orderLine.MenuItemId).IsRequired();
                line.Property(orderLine => orderLine.MenuItemName).HasMaxLength(160).IsRequired();
                line.Property(orderLine => orderLine.Quantity).IsRequired();
                line.Property(orderLine => orderLine.UnitPrice).HasPrecision(10, 2);
            });
        });
    }
}
