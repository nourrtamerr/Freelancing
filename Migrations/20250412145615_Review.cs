using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "IsDeleted",
            //    table: "freelancers");

            //migrationBuilder.AddColumn<string>(
            //    name: "Comment",
            //    table: "Reviews",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<int>(
            //    name: "Rating",
            //    table: "Reviews",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "20ecee9a-8d88-4799-8211-5c9c92add8ab", "AQAAAAIAAYagAAAAEGQD0GaLUgfBwci8VdkcgqTSysIlQzbX/AADe0omgbtvwG3c9OtrXb50HJTpaG78YA==", new DateTime(2025, 4, 12, 16, 56, 14, 292, DateTimeKind.Local).AddTicks(1308), "3759fae3-916c-4c5b-960f-0f2a97edd0c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Comment",
            //    table: "Reviews");

            //migrationBuilder.DropColumn(
            //    name: "Rating",
            //    table: "Reviews");

            //migrationBuilder.AddColumn<string>(
            //    name: "IsDeleted",
            //    table: "freelancers",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d441af2f-e419-4cd2-937c-5a9dbb33c963", "AQAAAAIAAYagAAAAEAQnMYN61Og7JqM1IpZT5c88svkV1AWHjyacYpOh6x0j1WPByjgSuO78oJB6wYXKPw==", new DateTime(2025, 4, 12, 2, 31, 36, 703, DateTimeKind.Local).AddTicks(6884), "d90a4313-a087-4ba9-b3d1-72e1ae1ce163" });
        }
    }
}
