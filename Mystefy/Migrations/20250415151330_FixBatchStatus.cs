using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class FixBatchStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Batches",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Batches");
        }
    }
}
