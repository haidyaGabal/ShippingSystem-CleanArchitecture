using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CarrierId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers",
                table: "TbShippmentStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "TbShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_CarrierId",
                table: "TbShipments",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers",
                table: "TbShipments",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers_CarrierId",
                table: "TbShippmentStatus",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers_CarrierId",
                table: "TbShippmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_TbShipments_CarrierId",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "TbShipments");
        }
    }
}
