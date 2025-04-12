using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class refreshtokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d441af2f-e419-4cd2-937c-5a9dbb33c963", "AQAAAAIAAYagAAAAEAQnMYN61Og7JqM1IpZT5c88svkV1AWHjyacYpOh6x0j1WPByjgSuO78oJB6wYXKPw==", "", new DateTime(2025, 4, 12, 2, 31, 36, 703, DateTimeKind.Local).AddTicks(6884), "d90a4313-a087-4ba9-b3d1-72e1ae1ce163" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "588d4854-46a2-4df0-9a6a-05f2f9128aa5", "AQAAAAIAAYagAAAAELu7d+owkhv5aiAAng4Qxnfl448tKTH9ylXD/0aEowd1H+hJZhZFDmB+VRShFivtvQ==", "4967d530-9b1b-4b76-820d-e30431c0b212" });
        }
    }
}
