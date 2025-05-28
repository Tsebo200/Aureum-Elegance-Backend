using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mystefy.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedManagerToWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedManagerUserId",
                table: "Warehouses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_AssignedManagerUserId",
                table: "Warehouses",
                column: "AssignedManagerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Users_AssignedManagerUserId",
                table: "Warehouses",
                column: "AssignedManagerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Users_AssignedManagerUserId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_AssignedManagerUserId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "AssignedManagerUserId",
                table: "Warehouses");
        }
    }
}
