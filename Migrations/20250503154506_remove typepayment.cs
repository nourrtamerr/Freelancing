using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class removetypepayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "a56841bd-f0a1-4de3-8448-d93264cb8777", "AQAAAAIAAYagAAAAEA8W/aOQhayAL9QhiZoQmqVyfjtd9KIV8gvjSUIbQRuTeuySHr2vP10rXvfP9vczoA==", new DateTime(2025, 5, 3, 18, 45, 3, 597, DateTimeKind.Local).AddTicks(9548), "4a4f5393-9523-48a3-a6f8-ce7572cec738" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "10aa621a-4550-4f5c-9a04-6f7992b1213b", "AQAAAAIAAYagAAAAEH/7BAQUlTIlySWL2zbESFs83O/y2stg4IKABtKKsL+or0r54AkAN29/l6QXHuE+8w==", new DateTime(2025, 5, 3, 17, 5, 42, 599, DateTimeKind.Local).AddTicks(2757), "2b5a1032-7714-4e3e-b255-bb169576e14f" });
        }
    }
}
