using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_items_condition_numbers_condition_number_id",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "fk_items_conditions_condition_id",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "fk_items_locations_location_id",
                table: "items");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                table: "items",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "condition_number_id",
                table: "items",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "condition_id",
                table: "items",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    location_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "full_name", "image_path", "location_id", "password_hash", "role", "updated_at", "username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Mendoza", "/images/users/alice.png", 1, "AdminPass123", "Admin", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.admin" },
                    { 2, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Reyes", "/images/users/john.png", 2, "UserPass123", "User", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.reyes" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_location_id",
                table: "users",
                column: "location_id");

            migrationBuilder.AddForeignKey(
                name: "fk_items_condition_numbers_condition_number_id",
                table: "items",
                column: "condition_number_id",
                principalTable: "condition_numbers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_items_conditions_condition_id",
                table: "items",
                column: "condition_id",
                principalTable: "conditions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_items_locations_location_id",
                table: "items",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_items_condition_numbers_condition_number_id",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "fk_items_conditions_condition_id",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "fk_items_locations_location_id",
                table: "items");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                table: "items",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "condition_number_id",
                table: "items",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "condition_id",
                table: "items",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_items_condition_numbers_condition_number_id",
                table: "items",
                column: "condition_number_id",
                principalTable: "condition_numbers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_items_conditions_condition_id",
                table: "items",
                column: "condition_id",
                principalTable: "conditions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_items_locations_location_id",
                table: "items",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id");
        }
    }
}
