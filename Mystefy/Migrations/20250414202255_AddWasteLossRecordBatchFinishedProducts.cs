using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class AddWasteLossRecordBatchFinishedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WasteLossRecordBatchFinishedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuantityLoss = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    DateOfLoss = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    BatchId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteLossRecordBatchFinishedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WLRBatchFinishedProducts_FinishedProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "FinishedProduct",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WasteLossRecordBatchFinishedProducts_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "BatchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WasteLossRecordBatchFinishedProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WasteLossRecordBatchFinishedProducts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WasteLossRecordBatchFinishedProducts_BatchId",
                table: "WasteLossRecordBatchFinishedProducts",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteLossRecordBatchFinishedProducts_ProductId",
                table: "WasteLossRecordBatchFinishedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteLossRecordBatchFinishedProducts_UserId",
                table: "WasteLossRecordBatchFinishedProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteLossRecordBatchFinishedProducts_WarehouseId",
                table: "WasteLossRecordBatchFinishedProducts",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteLossRecordBatchFinishedProducts");
        }
    }
}
