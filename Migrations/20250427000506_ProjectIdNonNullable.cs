using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ProjectIdNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "ProjectId",
            //    table: "Reviews",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "1",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
            //    values: new object[] { "097a6efb-5b41-462a-9b45-0e6102fd2793", "AQAAAAIAAYagAAAAEAe7TOlV/qE4Xj+rR8lG4+X/geXAh4osZsPqkVxC3sunNlYxU0fe6xzDulcMh5o3/g==", new DateTime(2025, 4, 27, 3, 5, 5, 899, DateTimeKind.Local).AddTicks(4044), "8ca6ffac-e23c-44c2-8ede-4cabe2559ba9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "ProjectId",
            //    table: "Reviews",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "1",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
            //    values: new object[] { "42068c4d-b403-4147-8c0b-1a717844283b", "AQAAAAIAAYagAAAAEBGMdUYuoiaqHmH9pp9P32st6CM5U4YTiZT6rvY67iMeU8eI9hFTde40jY0XjU3vog==", new DateTime(2025, 4, 27, 3, 3, 57, 321, DateTimeKind.Local).AddTicks(433), "c1883fc5-989f-49b1-8446-90aadfb45d56" });
        }
    }
}
