using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class fixdatabaseControllers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProduct_Fragrance_FragranceID",
                table: "FinishedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequest_Ingredients_IngredientsId",
                table: "StockRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequest_Users_UserId",
                table: "StockRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockRequest",
                table: "StockRequest");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Fragrance_TempId",
                table: "Fragrance");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "StockRequest",
                newName: "StockRequests");

            migrationBuilder.RenameTable(
                name: "Fragrance",
                newName: "Fragrances");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Ingredients",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_StockRequest_UserId",
                table: "StockRequests",
                newName: "IX_StockRequests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StockRequest_IngredientsId",
                table: "StockRequests",
                newName: "IX_StockRequests_IngredientsId");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "Fragrances",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Cost",
                table: "Ingredients",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Ingredients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BatchID",
                table: "FinishedProduct",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "StockRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Fragrances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Fragrances",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Fragrances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Fragrances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fragrances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "Fragrances",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockRequests",
                table: "StockRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fragrances",
                table: "Fragrances",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    BatchID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchID);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProduct_BatchID",
                table: "FinishedProduct",
                column: "BatchID");

            migrationBuilder.CreateIndex(
                name: "IX_StockRequests_WarehouseId",
                table: "StockRequests",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProduct_Batches_BatchID",
                table: "FinishedProduct",
                column: "BatchID",
                principalTable: "Batches",
                principalColumn: "BatchID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProduct_Fragrances_FragranceID",
                table: "FinishedProduct",
                column: "FragranceID",
                principalTable: "Fragrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequests_Ingredients_IngredientsId",
                table: "StockRequests",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequests_Users_UserId",
                table: "StockRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequests_Warehouses_WarehouseId",
                table: "StockRequests",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProduct_Batches_BatchID",
                table: "FinishedProduct");

            

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequests_Ingredients_IngredientsId",
                table: "StockRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequests_Users_UserId",
                table: "StockRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequests_Warehouses_WarehouseId",
                table: "StockRequests");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProduct_BatchID",
                table: "FinishedProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockRequests",
                table: "StockRequests");

            migrationBuilder.DropIndex(
                name: "IX_StockRequests_WarehouseId",
                table: "StockRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fragrances",
                table: "Fragrances");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "BatchID",
                table: "FinishedProduct");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "StockRequests");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Fragrances");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Fragrances");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Fragrances");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fragrances");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Fragrances");

            migrationBuilder.RenameTable(
                name: "StockRequests",
                newName: "StockRequest");

            migrationBuilder.RenameTable(
                name: "Fragrances",
                newName: "Fragrance");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Ingredients",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_StockRequests_UserId",
                table: "StockRequest",
                newName: "IX_StockRequest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StockRequests_IngredientsId",
                table: "StockRequest",
                newName: "IX_StockRequest_IngredientsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Fragrance",
                newName: "TempId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Ingredients",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Ingredients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "Ingredients",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "TempId",
                table: "Fragrance",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockRequest",
                table: "StockRequest",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Fragrance_TempId",
                table: "Fragrance",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProduct_Fragrance_FragranceID",
                table: "FinishedProduct",
                column: "FragranceID",
                principalTable: "Fragrance",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequest_Ingredients_IngredientsId",
                table: "StockRequest",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequest_Users_UserId",
                table: "StockRequest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
