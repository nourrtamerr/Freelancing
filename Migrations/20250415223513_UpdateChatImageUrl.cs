using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChatImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "28a59b3e-f1f4-4a0b-a9cd-0ffe74c1871a", "AQAAAAIAAYagAAAAECglvTv4PUAZF+DgCyRK47Hdousm13+a46KzkktifTl3Sh2GElkdYjF/KAigaYdckg==", new DateTime(2025, 4, 16, 0, 35, 7, 415, DateTimeKind.Local).AddTicks(9801), "42fc2d83-4b7c-40db-a8c6-51dc7fb24716" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d785bcfd-1ee8-4d5e-987d-83e17f5ab5de", "AQAAAAIAAYagAAAAEBY1yc5Q32Mp1rxXninKVsRWof6wp7CKxxQT18BbuvA7j9rGeDlS+fsoftYnGZNx+g==", new DateTime(2025, 4, 15, 20, 26, 48, 920, DateTimeKind.Local).AddTicks(7939), "3df7b69a-1290-45c5-9e52-ba6aa8864b94" });
        }
    }
}
