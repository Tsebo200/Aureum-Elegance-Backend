using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class fixFragrances3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FragranceID",
                table: "Fragrances",
                newName: "Id");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Fragrances",
                newName: "FragranceID");
        }
    }
}
