using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyHub.Payment.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApiKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreditCardInformation_CardNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreditCardInformation_ExpiryDate_Month = table.Column<int>(type: "int", nullable: true),
                    CreditCardInformation_ExpiryDate_Year = table.Column<int>(type: "int", nullable: true),
                    CreditCardInformation_Ccv = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.MerchantId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_CurrencyCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CreditCardInformation_CardNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreditCardInformation_ExpiryDate_Month = table.Column<int>(type: "int", nullable: true),
                    CreditCardInformation_ExpiryDate_Year = table.Column<int>(type: "int", nullable: true),
                    CreditCardInformation_Ccv = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    PaymentOrderUniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalShopperIdentifier = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
