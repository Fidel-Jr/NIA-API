using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModelItemConditionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item_condition_histories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_id = table.Column<int>(type: "integer", nullable: false),
                    qr_code_id = table.Column<Guid>(type: "uuid", nullable: false),
                    condition_id = table.Column<int>(type: "integer", nullable: false),
                    condition_number_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_condition_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_item_condition_histories_condition_numbers_condition_number",
                        column: x => x.condition_number_id,
                        principalTable: "condition_numbers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_condition_histories_conditions_condition_id",
                        column: x => x.condition_id,
                        principalTable: "conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_condition_histories_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_condition_histories_qr_codes_qr_code_id",
                        column: x => x.qr_code_id,
                        principalTable: "qr_codes",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_item_condition_histories_condition_id",
                table: "item_condition_histories",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_condition_histories_condition_number_id",
                table: "item_condition_histories",
                column: "condition_number_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_condition_histories_item_id",
                table: "item_condition_histories",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_condition_histories_qr_code_id",
                table: "item_condition_histories",
                column: "qr_code_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_condition_histories");
        }
    }
}
