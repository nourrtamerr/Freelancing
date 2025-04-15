using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class MakeImageUrlNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortofolioProjectImageDTO");

            migrationBuilder.DropTable(
                name: "UserSkillDto");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Chats",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d785bcfd-1ee8-4d5e-987d-83e17f5ab5de", "AQAAAAIAAYagAAAAEBY1yc5Q32Mp1rxXninKVsRWof6wp7CKxxQT18BbuvA7j9rGeDlS+fsoftYnGZNx+g==", new DateTime(2025, 4, 15, 20, 26, 48, 920, DateTimeKind.Local).AddTicks(7939), "3df7b69a-1290-45c5-9e52-ba6aa8864b94" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Chats",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PortofolioProjectImageDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortofolioProjectImageDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSkillDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreelancerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkillDto", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "9331e64f-dac9-47e0-b8e0-674d20900ea4", "AQAAAAIAAYagAAAAEAxFgKo8QgAHyWQqtYUTzTmWEkP8ZVLTDIeaLAuwL/PLW/quB4cMTAcfhcZaIjDcnA==", new DateTime(2025, 4, 15, 19, 4, 36, 164, DateTimeKind.Local).AddTicks(2619), "d7097195-a180-4a6b-aca2-d70fdd847b32" });
        }
    }
}
