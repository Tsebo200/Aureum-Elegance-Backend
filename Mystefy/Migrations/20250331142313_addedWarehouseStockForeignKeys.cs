using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class addedWarehouseStockForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FragranceID",
                table: "WarehouseStocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseID",
                table: "WarehouseStocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStocks_FragranceID",
                table: "WarehouseStocks",
                column: "FragranceID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStocks_WarehouseID",
                table: "WarehouseStocks",
                column: "WarehouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStocks_Fragrances_FragranceID",
                table: "WarehouseStocks",
                column: "FragranceID",
                principalTable: "Fragrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStocks_Warehouses_WarehouseID",
                table: "WarehouseStocks",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStocks_Fragrances_FragranceID",
                table: "WarehouseStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStocks_Warehouses_WarehouseID",
                table: "WarehouseStocks");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseStocks_FragranceID",
                table: "WarehouseStocks");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseStocks_WarehouseID",
                table: "WarehouseStocks");

            migrationBuilder.DropColumn(
                name: "FragranceID",
                table: "WarehouseStocks");

            migrationBuilder.DropColumn(
                name: "WarehouseID",
                table: "WarehouseStocks");
        }
    }
}
