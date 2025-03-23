using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class fixWarehouseTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Warehouses");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Warehouses",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "IngredientID",
                table: "Warehouses",
                newName: "WarehouseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Warehouses",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "WarehouseID",
                table: "Warehouses",
                newName: "IngredientID");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Warehouses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Warehouses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
