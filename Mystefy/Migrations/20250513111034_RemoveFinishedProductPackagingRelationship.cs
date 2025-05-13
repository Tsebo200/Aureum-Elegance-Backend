using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFinishedProductPackagingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key if it exists
    migrationBuilder.DropForeignKey(
        name: "FK_FinishedProduct_Packaging_PackagingId",
        table: "FinishedProduct");

    // Drop index on PackagingId (if EF created one)
    migrationBuilder.DropIndex(
        name: "IX_FinishedProduct_PackagingId",
        table: "FinishedProduct");

    // Drop the column itself
    migrationBuilder.DropColumn(
        name: "PackagingId",
        table: "FinishedProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
        name: "PackagingId",
        table: "FinishedProduct",
        type: "integer",
        nullable: true); // Or false if it was required

    migrationBuilder.CreateIndex(
        name: "IX_FinishedProduct_PackagingId",
        table: "FinishedProduct",
        column: "PackagingId");

    migrationBuilder.AddForeignKey(
        name: "FK_FinishedProduct_Packaging_PackagingId",
        table: "FinishedProduct",
        column: "PackagingId",
        principalTable: "Packaging",
        principalColumn: "Id");
        }
    }
}
