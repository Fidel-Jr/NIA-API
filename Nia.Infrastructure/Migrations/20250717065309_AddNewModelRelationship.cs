using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModelRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "condition_id",
                table: "items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "condition_number_id",
                table: "items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "location_id",
                table: "items",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "condition_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    condition_number_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_condition_numbers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "conditions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    condition_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conditions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    location_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_name", "created_at", "updated_at" },
                values: new object[,]
                {
                    { 1, "Desktop", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Consumables", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "ICT", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "condition_numbers",
                columns: new[] { "id", "condition_number_type", "created_at", "updated_at" },
                values: new object[,]
                {
                    { 1, "A1", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "A2", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "A3", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "A4", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "A5", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "conditions",
                columns: new[] { "id", "condition_name", "created_at", "updated_at" },
                values: new object[,]
                {
                    { 1, "Serviceable", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Non - Serviceable", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "On Maintenance", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "locations",
                columns: new[] { "id", "created_at", "location_name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Billing Unit", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "O & M", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin & Finance", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "IDDD", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Equipment", new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_items_category_id",
                table: "items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_condition_id",
                table: "items",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_condition_number_id",
                table: "items",
                column: "condition_number_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_location_id",
                table: "items",
                column: "location_id");

            migrationBuilder.AddForeignKey(
                name: "fk_items_categories_category_id",
                table: "items",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_items_categories_category_id",
                table: "items");

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
                name: "categories");

            migrationBuilder.DropTable(
                name: "condition_numbers");

            migrationBuilder.DropTable(
                name: "conditions");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropIndex(
                name: "ix_items_category_id",
                table: "items");

            migrationBuilder.DropIndex(
                name: "ix_items_condition_id",
                table: "items");

            migrationBuilder.DropIndex(
                name: "ix_items_condition_number_id",
                table: "items");

            migrationBuilder.DropIndex(
                name: "ix_items_location_id",
                table: "items");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "items");

            migrationBuilder.DropColumn(
                name: "condition_id",
                table: "items");

            migrationBuilder.DropColumn(
                name: "condition_number_id",
                table: "items");

            migrationBuilder.DropColumn(
                name: "location_id",
                table: "items");
        }
    }
}
