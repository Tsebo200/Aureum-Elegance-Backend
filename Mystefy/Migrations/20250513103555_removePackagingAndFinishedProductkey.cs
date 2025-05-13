using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class removePackagingAndFinishedProductkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProduct_Packaging_PackagingID",
                table: "FinishedProduct");

            migrationBuilder.RenameColumn(
                name: "PackagingID",
                table: "FinishedProduct",
                newName: "PackagingId");

            migrationBuilder.RenameIndex(
                name: "IX_FinishedProduct_PackagingID",
                table: "FinishedProduct",
                newName: "IX_FinishedProduct_PackagingId");

            migrationBuilder.AlterColumn<int>(
                name: "PackagingId",
                table: "FinishedProduct",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProduct_Packaging_PackagingId",
                table: "FinishedProduct",
                column: "PackagingId",
                principalTable: "Packaging",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProduct_Packaging_PackagingId",
                table: "FinishedProduct");

            migrationBuilder.RenameColumn(
                name: "PackagingId",
                table: "FinishedProduct",
                newName: "PackagingID");

            migrationBuilder.RenameIndex(
                name: "IX_FinishedProduct_PackagingId",
                table: "FinishedProduct",
                newName: "IX_FinishedProduct_PackagingID");

            migrationBuilder.AlterColumn<int>(
                name: "PackagingID",
                table: "FinishedProduct",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProduct_Packaging_PackagingID",
                table: "FinishedProduct",
                column: "PackagingID",
                principalTable: "Packaging",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
