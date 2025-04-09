using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class BatchFixAndBatchFinishedProductsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProduct_Batches_BatchID",
                table: "FinishedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequests_Users_UserId",
                table: "StockRequests");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProduct_BatchID",
                table: "FinishedProduct");

            migrationBuilder.DropColumn(
                name: "BatchID",
                table: "FinishedProduct");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Batches");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StockRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BatchSize",
                table: "Batches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BatchFinishedProducts",
                columns: table => new
                {
                    BatchID = table.Column<int>(type: "integer", nullable: false),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    WarehouseID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchFinishedProducts", x => new { x.BatchID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_BatchFinishedProducts_Batches_BatchID",
                        column: x => x.BatchID,
                        principalTable: "Batches",
                        principalColumn: "BatchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchFinishedProducts_FinishedProduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "FinishedProduct",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchFinishedProducts_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchFinishedProducts_ProductID",
                table: "BatchFinishedProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_BatchFinishedProducts_WarehouseID",
                table: "BatchFinishedProducts",
                column: "WarehouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequests_Users_UserId",
                table: "StockRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockRequests_Users_UserId",
                table: "StockRequests");

            migrationBuilder.DropTable(
                name: "BatchFinishedProducts");


            migrationBuilder.DropColumn(
                name: "BatchSize",
                table: "Batches");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StockRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BatchID",
                table: "FinishedProduct",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Batches",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProduct_BatchID",
                table: "FinishedProduct",
                column: "BatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProduct_Batches_BatchID",
                table: "FinishedProduct",
                column: "BatchID",
                principalTable: "Batches",
                principalColumn: "BatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequests_Users_UserId",
                table: "StockRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
