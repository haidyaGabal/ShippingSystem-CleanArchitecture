using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers_CarrierId",
                table: "TbShippmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_TbShippmentStatus_CarrierId",
                table: "TbShippmentStatus");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "TbShippmentStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "TbShippmentStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TbShippmentStatus_CarrierId",
                table: "TbShippmentStatus",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers_CarrierId",
                table: "TbShippmentStatus",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
