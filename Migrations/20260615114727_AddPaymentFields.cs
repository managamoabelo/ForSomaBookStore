using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForSomaBookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentReference",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportReason",
                table: "Textbooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReportReviewed",
                table: "Textbooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentReference",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReportReason",
                table: "Textbooks");

            migrationBuilder.DropColumn(
                name: "ReportReviewed",
                table: "Textbooks");
        }
    }
}
