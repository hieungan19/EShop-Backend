using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Migrations
{
    /// <inheritdoc />
    public partial class AddPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantWebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantIpnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantReturnUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedByy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    DesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DesLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    PaymentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentDestinationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLastMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotiMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiPaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiMerchantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiNotiStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiResMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResHttpCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    SignValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignAlgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignOwn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
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
                    TranMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "192990e9-0055-496c-9db7-a522ffa33372");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "605ca164-0c14-43fb-8a0e-5aff4cc84c04");
        }
    }
}
