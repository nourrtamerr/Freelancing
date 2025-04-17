using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ClinetBalanceAndStripeAccId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "clients",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StripeAccountId",
                table: "clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "db00cc72-5091-47f5-bb7d-ad332700adc7", "AQAAAAIAAYagAAAAEMtK9w4udOFxIugOuUkurWIBJSi2u47PmNBVUHiVUQyAH4GPYviT77/xl97OvlvOCQ==", new DateTime(2025, 4, 17, 18, 12, 27, 268, DateTimeKind.Local).AddTicks(2974), "afb7e80b-61ad-46eb-84bb-ba6aa12f3c1e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "StripeAccountId",
                table: "clients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "bb6d772d-3c87-4913-a3ee-c5260f4fbcc6", "AQAAAAIAAYagAAAAEFfXqmlqsWUgz6C+F+Q+KslVRBKa8QBsYk0S5YYpDhWDyLTgrKtgyVGiCO28UHKEKA==", new DateTime(2025, 4, 17, 17, 1, 51, 13, DateTimeKind.Local).AddTicks(2095), "31fe4802-ee1d-49cb-8344-bdfbd6049c38" });
        }
    }
}
