using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_items_qr_codes_qr_code_guid",
                table: "items");

            migrationBuilder.DropIndex(
                name: "ix_items_qr_code_guid",
                table: "items");

            migrationBuilder.DropColumn(
                name: "qr_code_guid",
                table: "items");

            migrationBuilder.CreateIndex(
                name: "ix_qr_codes_item_id",
                table: "qr_codes",
                column: "item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_qr_codes_items_item_id",
                table: "qr_codes",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_qr_codes_items_item_id",
                table: "qr_codes");

            migrationBuilder.DropIndex(
                name: "ix_qr_codes_item_id",
                table: "qr_codes");

            migrationBuilder.AddColumn<Guid>(
                name: "qr_code_guid",
                table: "items",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_items_qr_code_guid",
                table: "items",
                column: "qr_code_guid");

            migrationBuilder.AddForeignKey(
                name: "fk_items_qr_codes_qr_code_guid",
                table: "items",
                column: "qr_code_guid",
                principalTable: "qr_codes",
                principalColumn: "guid");
        }
    }
}
