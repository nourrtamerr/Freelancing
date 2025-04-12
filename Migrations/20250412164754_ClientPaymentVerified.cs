using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ClientPaymentVerified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaymentVerified",
                table: "clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "b0c485c4-0d15-48f6-b4ed-bba6565370f0", "AQAAAAIAAYagAAAAEIwtusojaPCcv1JIdszQMS5mfLtu2QSOV6xqSKExp0zKN81y94aP9hc3ODoV4ZFrSw==", new DateTime(2025, 4, 12, 18, 47, 53, 940, DateTimeKind.Local).AddTicks(3813), "210eec2c-9760-4a37-9e38-12ad834de519" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentVerified",
                table: "clients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "20ecee9a-8d88-4799-8211-5c9c92add8ab", "AQAAAAIAAYagAAAAEGQD0GaLUgfBwci8VdkcgqTSysIlQzbX/AADe0omgbtvwG3c9OtrXb50HJTpaG78YA==", new DateTime(2025, 4, 12, 16, 56, 14, 292, DateTimeKind.Local).AddTicks(1308), "3759fae3-916c-4c5b-960f-0f2a97edd0c2" });
        }
    }
}
