using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class stripeaccountidfreelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeAccountId",
                table: "freelancers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "84c466f7-cbbe-459f-b4b1-b3f5ba571512", "AQAAAAIAAYagAAAAEJuLw4YySZjoYH7wnsqTXxBoXl/rn1QQu8pNhr8qEW9MmMBJikWjkXxd4yuTlUxeEg==", new DateTime(2025, 4, 17, 1, 24, 17, 836, DateTimeKind.Local).AddTicks(9492), "02c2e1ea-dda4-4cb3-823d-9f78a4ebefde" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeAccountId",
                table: "freelancers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "38a6ad05-63a4-4e03-bf40-38269c5bd32e", "AQAAAAIAAYagAAAAEDED5elCyRYnReCNMSbUgYU3tx7SPai/YiXEqWVKk896verFMM/pRoO8sUrxDCKRIQ==", new DateTime(2025, 4, 16, 23, 34, 34, 118, DateTimeKind.Local).AddTicks(1687), "00350cc0-60a7-4c3b-b34e-c4cb019b36e4" });
        }
    }
}
