using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class shwyt7agat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Milestones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "IsVerified", "NationalId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96fa1d22-15fc-49ee-ae9d-a2d72dde0bfb", false, null, "AQAAAAIAAYagAAAAELwhapaH68Y4Ad/K3QzL34mltOxGFnh6mLdMVepJxCeMixT4hMjsL8/DkSUIL3PdOg==", "c974c1f2-7bb0-4d3e-a632-7836742415cf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d71c26cf-06bc-4c3a-9ede-fc401b57b250", "AQAAAAIAAYagAAAAEI+40O+DkbSUIbLrWwVpYUhpUnpEsf8XlweSE3SK0jO8krtDSjsH8Y+VpIESiTBVjA==", "52fc2b80-8f48-430f-9027-51227c7290be" });
        }
    }
}
