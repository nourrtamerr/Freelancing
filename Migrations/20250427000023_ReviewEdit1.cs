using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ReviewEdit1 : Migration
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
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "7258fdb6-d271-42e6-9c05-b9a6f66feea9", "AQAAAAIAAYagAAAAEP8RfQIj+4V7XYayG5A+Lk/6gFvUZBftjlsYUrk2XQwMOa+EN90yuKKpAekdtgHuBw==", new DateTime(2025, 4, 27, 3, 0, 22, 518, DateTimeKind.Local).AddTicks(3179), "05cb03af-e356-40a3-bb74-1739b654eac0" });

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
