using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTbShipingPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbShippments",
                table: "TbShippmentStatus");

            migrationBuilder.DropTable(
                name: "TbShippments");

            migrationBuilder.DropTable(
                name: "TbShippingTypes");

            migrationBuilder.DropTable(
                name: "TbUserSebders");

            migrationBuilder.RenameColumn(
                name: "ShippmentId",
                table: "TbShippmentStatus",
                newName: "ShipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TbShippmentStatus_ShippmentId",
                table: "TbShippmentStatus",
                newName: "IX_TbShippmentStatus_ShipmentId");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "TbUserReceivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtherAddress",
                table: "TbUserReceivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "TbUserReceivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TbShipingPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TbShipingPackagesAname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TbShipingPackagesEname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShipingPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbShipingTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ShippingTypeAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShippingTypeEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShipingFactor = table.Column<double>(type: "float", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShipingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbUserSenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserSenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserSebders_TbCities",
                        column: x => x.CityId,
                        principalTable: "TbCities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbShipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ShippingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipingPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    PackageValue = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ShipingRate = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrackingNumber = table.Column<double>(type: "float", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShipingPackagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbShipments_TbShipingPackages_ShipingPackagesId",
                        column: x => x.ShipingPackagesId,
                        principalTable: "TbShipingPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbShippments_TbPaymentMethods",
                        column: x => x.PaymentMethodId,
                        principalTable: "TbPaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbShippingTypes",
                        column: x => x.ShipingTypeId,
                        principalTable: "TbShipingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbUserReceivers",
                        column: x => x.ReceiverId,
                        principalTable: "TbUserReceivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbUserSebders",
                        column: x => x.SenderId,
                        principalTable: "TbUserSenders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_PaymentMethodId",
                table: "TbShipments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_ReceiverId",
                table: "TbShipments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_SenderId",
                table: "TbShipments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_ShipingPackagesId",
                table: "TbShipments",
                column: "ShipingPackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_ShipingTypeId",
                table: "TbShipments",
                column: "ShipingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserSenders_CityId",
                table: "TbUserSenders",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbShippments",
                table: "TbShippmentStatus",
                column: "ShipmentId",
                principalTable: "TbShipments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbShippments",
                table: "TbShippmentStatus");

            migrationBuilder.DropTable(
                name: "TbShipments");

            migrationBuilder.DropTable(
                name: "TbShipingPackages");

            migrationBuilder.DropTable(
                name: "TbShipingTypes");

            migrationBuilder.DropTable(
                name: "TbUserSenders");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "TbUserReceivers");

            migrationBuilder.DropColumn(
                name: "OtherAddress",
                table: "TbUserReceivers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "TbUserReceivers");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "TbShippmentStatus",
                newName: "ShippmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TbShippmentStatus_ShipmentId",
                table: "TbShippmentStatus",
                newName: "IX_TbShippmentStatus_ShippmentId");

            migrationBuilder.CreateTable(
                name: "TbShippingTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    ShippingFactor = table.Column<double>(type: "float", nullable: false),
                    ShippingTypeAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShippingTypeEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShippingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbUserSebders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserSebders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserSebders_TbCities",
                        column: x => x.CityId,
                        principalTable: "TbCities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbShippments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    PackageValue = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ShippingRate = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    TrackingNumber = table.Column<double>(type: "float", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShippments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbShippments_TbPaymentMethods",
                        column: x => x.PaymentMethodId,
                        principalTable: "TbPaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbShippingTypes",
                        column: x => x.ShippingTypeId,
                        principalTable: "TbShippingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbUserReceivers",
                        column: x => x.ReceiverId,
                        principalTable: "TbUserReceivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbUserSebders",
                        column: x => x.SenderId,
                        principalTable: "TbUserSebders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_PaymentMethodId",
                table: "TbShippments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_ReceiverId",
                table: "TbShippments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_SenderId",
                table: "TbShippments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_ShippingTypeId",
                table: "TbShippments",
                column: "ShippingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserSebders_CityId",
                table: "TbUserSebders",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbShippments",
                table: "TbShippmentStatus",
                column: "ShippmentId",
                principalTable: "TbShippments",
                principalColumn: "Id");
        }
    }
}
