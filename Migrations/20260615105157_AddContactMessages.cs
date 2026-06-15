using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForSomaBookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddContactMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_BuyerId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Textbooks_AspNetUsers_UserId",
                table: "Textbooks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Textbooks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ApplicationUserId",
                table: "Offers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_ApplicationUserId",
                table: "Offers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_BuyerId",
                table: "Offers",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Textbooks_AspNetUsers_UserId",
                table: "Textbooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_ApplicationUserId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_BuyerId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Textbooks_AspNetUsers_UserId",
                table: "Textbooks");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ApplicationUserId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Textbooks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_BuyerId",
                table: "Offers",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Textbooks_AspNetUsers_UserId",
                table: "Textbooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
