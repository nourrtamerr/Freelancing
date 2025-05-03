using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class updatepayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp", "Title" },
                values: new object[] { "9c04d6d8-4e7f-4408-8d89-2ffc14a2ca7e", "AQAAAAIAAYagAAAAEJsA6JHNf5OB5A2JLR9WEbet39sQ9oS6/gqNG6xoYWZDxjW7hkjLPmnnHGrALJoycw==", new DateTime(2025, 5, 2, 16, 45, 56, 440, DateTimeKind.Local).AddTicks(9225), "1f1b789f-901a-4c72-a2e3-269d91de153d", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp", "Title" },
                values: new object[] { "cec641ed-0a83-4d1a-803b-94b397851929", "AQAAAAIAAYagAAAAEE8yituCwpOQea8vzXfidzRdLYiKw20/1H4KxZna1CY6zo67QO50dtgTIo5v0KqVGA==", new DateTime(2025, 4, 29, 2, 11, 27, 794, DateTimeKind.Local).AddTicks(3953), "10ae568e-5647-43ff-a028-1e2429ee3e34", null });
        }
    }
}
