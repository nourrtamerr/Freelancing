using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class SuggestedMilestoneTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "suggestedMilestones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "f275dce7-1a27-416f-b51f-c8e2cfba28f5", "AQAAAAIAAYagAAAAEHEcmsxK9rW/jj5ky0nQBejt4MLK8SGyk2nsnQYzPp3lpluCN+g0xsGn8tNTwrxgCw==", new DateTime(2025, 4, 17, 19, 8, 27, 176, DateTimeKind.Local).AddTicks(6214), "8b3c8f59-3bfc-42fd-9d12-bbbff11d6ef7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "suggestedMilestones");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "db00cc72-5091-47f5-bb7d-ad332700adc7", "AQAAAAIAAYagAAAAEMtK9w4udOFxIugOuUkurWIBJSi2u47PmNBVUHiVUQyAH4GPYviT77/xl97OvlvOCQ==", new DateTime(2025, 4, 17, 18, 12, 27, 268, DateTimeKind.Local).AddTicks(2974), "afb7e80b-61ad-46eb-84bb-ba6aa12f3c1e" });
        }
    }
}
