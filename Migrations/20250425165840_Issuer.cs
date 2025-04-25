using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Issuer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "7fd0153b-c5f0-4047-a5ce-7a6730036db7", "AQAAAAIAAYagAAAAEFSu7YkhhdIZsYM9UDmQITQP0lwSp1eckh3c3B1VKYjuoN+P73IGFHrdiebY7kM+Yw==", new DateTime(2025, 4, 25, 18, 58, 38, 298, DateTimeKind.Local).AddTicks(9932), "12204e35-6065-4ee9-9855-6c2ff7118320" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "certificates");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "65952636-9d35-416e-a4a7-f2123d251fcb", "AQAAAAIAAYagAAAAEAHxQqGHENGksaUnJbi3qOBm/IVQF49UKBdx6Tg8mZKCXuwxy+wZg1U3mgyD6YXYhQ==", new DateTime(2025, 4, 24, 23, 54, 57, 793, DateTimeKind.Local).AddTicks(6489), "57a0d203-4eb7-4ec2-b25c-abdb658aae8e" });
        }
    }
}
