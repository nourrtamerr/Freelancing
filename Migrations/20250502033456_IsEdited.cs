using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class IsEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp", "Title" },
                values: new object[] { "91494d14-d124-448f-97c5-0ccd6629aef9", "AQAAAAIAAYagAAAAEAz1kV4tOTHoA/IAAhgIqDB4OhbUA9kMKXV5N0ll5zTFEqzK9POLLfxu42JYozg9gQ==", new DateTime(2025, 5, 2, 6, 34, 43, 357, DateTimeKind.Local).AddTicks(1914), "3a1375e5-a42c-40f8-9fe8-3152f66b00b0", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Chats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp", "Title" },
                values: new object[] { "cec641ed-0a83-4d1a-803b-94b397851929", "AQAAAAIAAYagAAAAEE8yituCwpOQea8vzXfidzRdLYiKw20/1H4KxZna1CY6zo67QO50dtgTIo5v0KqVGA==", new DateTime(2025, 4, 29, 2, 11, 27, 794, DateTimeKind.Local).AddTicks(3953), "10ae568e-5647-43ff-a028-1e2429ee3e34", null });
        }
    }
}
