using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ReviewEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "42068c4d-b403-4147-8c0b-1a717844283b", "AQAAAAIAAYagAAAAEBGMdUYuoiaqHmH9pp9P32st6CM5U4YTiZT6rvY67iMeU8eI9hFTde40jY0XjU3vog==", new DateTime(2025, 4, 27, 3, 3, 57, 321, DateTimeKind.Local).AddTicks(433), "c1883fc5-989f-49b1-8446-90aadfb45d56" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_project_ProjectId",
                table: "Reviews",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_project_ProjectId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "7fd0153b-c5f0-4047-a5ce-7a6730036db7", "AQAAAAIAAYagAAAAEFSu7YkhhdIZsYM9UDmQITQP0lwSp1eckh3c3B1VKYjuoN+P73IGFHrdiebY7kM+Yw==", new DateTime(2025, 4, 25, 18, 58, 38, 298, DateTimeKind.Local).AddTicks(9932), "12204e35-6065-4ee9-9855-6c2ff7118320" });
        }
    }
}
