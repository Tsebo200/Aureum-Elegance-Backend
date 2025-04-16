using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class AddPackagingIdToStockRequestPackagings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockRequestPackagings_Ingredients_IngredientsId",
                table: "StockRequestPackagings");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientsId",
                table: "StockRequestPackagings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
                
// ⚠️ DO NOT add the PackagingId column again — since it already exists.
            // migrationBuilder.AddColumn<int>(
            //     name: "PackagingId",
            //     table: "StockRequestPackagings",
            //     type: "integer",
            //     nullable: false,
            //     defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockRequestPackagings_PackagingId",
                table: "StockRequestPackagings",
                column: "PackagingId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequestPackagings_Ingredients_IngredientsId",
                table: "StockRequestPackagings",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequestPackagings_Packaging_PackagingId",
                table: "StockRequestPackagings",
                column: "PackagingId",
                principalTable: "Packaging",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockRequestPackagings_Ingredients_IngredientsId",
                table: "StockRequestPackagings");

            migrationBuilder.DropForeignKey(
                name: "FK_StockRequestPackagings_Packaging_PackagingId",
                table: "StockRequestPackagings");

            migrationBuilder.DropIndex(
                name: "IX_StockRequestPackagings_PackagingId",
                table: "StockRequestPackagings");

            migrationBuilder.DropColumn(
                name: "PackagingId",
                table: "StockRequestPackagings");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientsId",
                table: "StockRequestPackagings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockRequestPackagings_Ingredients_IngredientsId",
                table: "StockRequestPackagings",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
