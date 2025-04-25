using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class aspnetusertitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp", "Title" },
                values: new object[] { "65952636-9d35-416e-a4a7-f2123d251fcb", "AQAAAAIAAYagAAAAEAHxQqGHENGksaUnJbi3qOBm/IVQF49UKBdx6Tg8mZKCXuwxy+wZg1U3mgyD6YXYhQ==", new DateTime(2025, 4, 24, 23, 54, 57, 793, DateTimeKind.Local).AddTicks(6489), "57a0d203-4eb7-4ec2-b25c-abdb658aae8e", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "701ab99f-3adf-4fa6-a5f3-7a3b14d20eee", "AQAAAAIAAYagAAAAEE8suFD6ihhRZCxGGe6DC2RAx9S0qXxD9H0ILEdymSdczHQZU2PWnC7Py41zq7omXQ==", new DateTime(2025, 4, 23, 23, 41, 7, 514, DateTimeKind.Local).AddTicks(6381), "46d2c703-ed80-4370-95a8-e479017bfe02" });
        }
    }
}
