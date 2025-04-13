using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fixedPrice",
                table: "fixedPriceProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "278ddb38-e885-46ff-a5d5-4fe5950c743b", "AQAAAAIAAYagAAAAEIzeTWTfc2PBM2D3dKru7eD7m4P/Q36CYcTIU20hr+7q4L7i4hmNHBrbnPYvzDGboQ==", new DateTime(2025, 4, 13, 16, 46, 21, 217, DateTimeKind.Local).AddTicks(7392), "131ac5f3-ea70-425d-847d-8b68f8438fdc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fixedPrice",
                table: "fixedPriceProjects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d68dd655-561a-4add-9bfd-9c55fdf87bca", "AQAAAAIAAYagAAAAELUag73Ev0rjQmrEG5/ib8vhDkn4oPOCgdgjaNWUIvWN/S1XnbAcZwXyU1E9mwx/sw==", new DateTime(2025, 4, 13, 15, 52, 45, 686, DateTimeKind.Local).AddTicks(3498), "8528c7fd-3805-451d-90ec-813b8dba578b" });
        }
    }
}
