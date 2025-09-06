using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddItemModelQty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "items",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "items");
        }
    }
}
