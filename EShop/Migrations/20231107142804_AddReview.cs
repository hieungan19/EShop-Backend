using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Migrations
{
    /// <inheritdoc />
    public partial class AddReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDestinations_Payments_PaymentId",
                table: "PaymentDestinations");

            migrationBuilder.DropTable(
                name: "PaymentNotifications");

            migrationBuilder.DropTable(
                name: "PaymentSignatures");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "PaymentDestinations");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrderPaymentInfo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Star = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "058e8de4-8b1b-4cb9-82a7-816a0bdb53bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8e754577-79f0-46ce-a2df-4cae034ef177");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderPaymentInfo",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedByy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantIpnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantReturnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantWebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDestinations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DesParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DesLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDestinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDestinations_PaymentDestinations_DesParentId",
                        column: x => x.DesParentId,
                        principalTable: "PaymentDestinations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MerchantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentDestinationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLastMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PaymentDestinations_PaymentDestinationId",
                        column: x => x.PaymentDestinationId,
                        principalTable: "PaymentDestinations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentNotifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotiMerchantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiPaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotiContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiNotiStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiResHttpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentNotifications_Merchants_NotiMerchantId",
                        column: x => x.NotiMerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentNotifications_Payments_NotiPaymentId",
                        column: x => x.NotiPaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentSignatures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    SignAlgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SignOwn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSignatures_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TranAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ebf7df23-de8a-44d7-bbee-81b1b0445513");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e5a9f564-482b-4394-ae29-d8c036df04b1");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDestinations_DesParentId",
                table: "PaymentDestinations",
                column: "DesParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDestinations_PaymentId",
                table: "PaymentDestinations",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotifications_NotiMerchantId",
                table: "PaymentNotifications",
                column: "NotiMerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotifications_NotiPaymentId",
                table: "PaymentNotifications",
                column: "NotiPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MerchantId",
                table: "Payments",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentDestinationId",
                table: "Payments",
                column: "PaymentDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSignatures_PaymentId",
                table: "PaymentSignatures",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDestinations_Payments_PaymentId",
                table: "PaymentDestinations",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
