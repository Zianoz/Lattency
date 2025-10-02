using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lattency.Migrations
{
    /// <inheritdoc />
    public partial class AvailabilityToCafeTableModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "CafeTables",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "CafeTables");
        }
    }
}
