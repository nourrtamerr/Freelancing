using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentimentPrediction",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SentimentProbability",
                table: "Reviews",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "29d5f95c-9b5f-4bc7-ae10-978741d97db6", "AQAAAAIAAYagAAAAEI267mSY+ntT8yt+T5oE7cT97xgkmhdj4PbNY7dNiaDBdjH43YC1Gk3okwZK659m2Q==", new DateTime(2025, 5, 3, 22, 10, 13, 171, DateTimeKind.Local).AddTicks(7558), "5447e96c-fda1-4103-8aed-e2755357681d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentPrediction",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "SentimentProbability",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "a56841bd-f0a1-4de3-8448-d93264cb8777", "AQAAAAIAAYagAAAAEA8W/aOQhayAL9QhiZoQmqVyfjtd9KIV8gvjSUIbQRuTeuySHr2vP10rXvfP9vczoA==", new DateTime(2025, 5, 3, 18, 45, 3, 597, DateTimeKind.Local).AddTicks(9548), "4a4f5393-9523-48a3-a6f8-ce7572cec738" });
        }
    }
}
