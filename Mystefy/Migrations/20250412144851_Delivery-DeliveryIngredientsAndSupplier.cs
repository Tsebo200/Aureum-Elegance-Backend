using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryDeliveryIngredientsAndSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientsId",
                table: "WarehouseIngredients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierName = table.Column<string>(type: "text", nullable: false),
                    ContactPerson = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    DeliveryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierID = table.Column<int>(type: "integer", nullable: false),
                    DeliveryDateArrived = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryDateOrdered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryCost = table.Column<decimal>(type: "numeric", nullable: false),
                    WarehouseID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.DeliveryID);
                    table.ForeignKey(
                        name: "FK_Deliveries_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryIngredients",
                columns: table => new
                {
                    DeliveryIngredientID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientID = table.Column<int>(type: "integer", nullable: false),
                    QuantityDelivered = table.Column<decimal>(type: "numeric", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryIngredientCost = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryIngredients", x => x.DeliveryIngredientID);
                    table.ForeignKey(
                        name: "FK_DeliveryIngredients_Deliveries_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryIngredients_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseIngredients_IngredientsId",
                table: "WarehouseIngredients",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SupplierID",
                table: "Deliveries",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_WarehouseID",
                table: "Deliveries",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryIngredients_DeliveryID",
                table: "DeliveryIngredients",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryIngredients_IngredientID",
                table: "DeliveryIngredients",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseIngredients_Ingredients_IngredientsId",
                table: "WarehouseIngredients",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseIngredients_Ingredients_IngredientsId",
                table: "WarehouseIngredients");

            migrationBuilder.DropTable(
                name: "DeliveryIngredients");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseIngredients_IngredientsId",
                table: "WarehouseIngredients");

            migrationBuilder.DropColumn(
                name: "IngredientsId",
                table: "WarehouseIngredients");
        }
    }
}
