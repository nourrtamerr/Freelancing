using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class mm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "1fb71671-9148-428a-a9de-65e8131e197e", "AQAAAAIAAYagAAAAELsEymShHRhtvoe/eGEgoBHkKfmwyuvixSsczIj7rgvqqrJ6cjSY9Us59jBqRk6mpg==", new DateTime(2025, 5, 3, 23, 50, 3, 783, DateTimeKind.Local).AddTicks(6116), "06b3135f-299c-4f39-a83f-2af5cf4ccffd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "29d5f95c-9b5f-4bc7-ae10-978741d97db6", "AQAAAAIAAYagAAAAEI267mSY+ntT8yt+T5oE7cT97xgkmhdj4PbNY7dNiaDBdjH43YC1Gk3okwZK659m2Q==", new DateTime(2025, 5, 3, 22, 10, 13, 171, DateTimeKind.Local).AddTicks(7558), "5447e96c-fda1-4103-8aed-e2755357681d" });
        }
    }
}
