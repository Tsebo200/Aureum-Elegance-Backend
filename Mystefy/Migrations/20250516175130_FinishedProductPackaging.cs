using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class FinishedProductPackaging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinishedProductPackaging",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    PackagingID = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProductPackaging", x => new { x.ProductID, x.PackagingID });
                    table.ForeignKey(
                        name: "FK_FinishedProductPackaging_FinishedProduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "FinishedProduct",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedProductPackaging_Packaging_PackagingID",
                        column: x => x.PackagingID,
                        principalTable: "Packaging",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProductPackaging_PackagingID",
                table: "FinishedProductPackaging",
                column: "PackagingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishedProductPackaging");
        }
    }
}
