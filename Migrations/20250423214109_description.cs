using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "freelancers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Description", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "701ab99f-3adf-4fa6-a5f3-7a3b14d20eee", "", "AQAAAAIAAYagAAAAEE8suFD6ihhRZCxGGe6DC2RAx9S0qXxD9H0ILEdymSdczHQZU2PWnC7Py41zq7omXQ==", new DateTime(2025, 4, 23, 23, 41, 7, 514, DateTimeKind.Local).AddTicks(6381), "46d2c703-ed80-4370-95a8-e479017bfe02" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "freelancers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "c8a044e4-b342-496c-be03-b27f5772703b", "AQAAAAIAAYagAAAAEJczzlGWLt3BbmlKZYAymXzujq1CRf1l0P1TFHqyLBn35h9d1GYKXTguQ+NmOB9DEA==", new DateTime(2025, 4, 23, 17, 40, 29, 509, DateTimeKind.Local).AddTicks(5300), "9be57ae2-82d5-4541-b4b2-4523e40e5f73" });
        }
    }
}
