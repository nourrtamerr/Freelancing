using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ChatImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Chats",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

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
                values: new object[] { "b4784e21-7671-49a3-9d72-11a128b96847", "AQAAAAIAAYagAAAAENLotUSuVGOCjcJr+c9j5hjSky9UnAi1ZPo+Uu1vizFJMg2Q0bc3weTsB5UzHaJaiA==", new DateTime(2025, 4, 15, 19, 0, 20, 278, DateTimeKind.Local).AddTicks(5319), "83f1dca4-59ac-4152-895c-642b68450cca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortofolioProjectImageDTO");

            migrationBuilder.DropTable(
                name: "UserSkillDto");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Chats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "7ef81e21-4599-4ebb-a148-fc4c8a17df60", "AQAAAAIAAYagAAAAECAgyvQ4cm5XAvsHpKFujlY2cR4dKea6oghqeWIhfbpx0mtVE93CTuz7qyGSefOHmQ==", new DateTime(2025, 4, 14, 1, 16, 40, 633, DateTimeKind.Local).AddTicks(7372), "ff6f58b0-d592-43a5-ab7d-d08cbdbf041b" });
        }
    }
}
