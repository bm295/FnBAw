using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FnBManagement.Web.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "InventoryItems",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                Unit = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                QuantityInStock = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                ReorderLevel = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryItems", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MenuItems",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                Category = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                IsAvailable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MenuItems", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                OrderedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OrderLines",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                OrderId = table.Column<int>(type: "integer", nullable: false),
                MenuItemId = table.Column<int>(type: "integer", nullable: false),
                MenuItemName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                UnitPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderLines", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderLines_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_OrderId",
            table: "OrderLines",
            column: "OrderId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "InventoryItems");

        migrationBuilder.DropTable(
            name: "MenuItems");

        migrationBuilder.DropTable(
            name: "OrderLines");

        migrationBuilder.DropTable(
            name: "Orders");
    }
}
