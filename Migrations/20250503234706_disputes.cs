using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class disputes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isResolved",
                table: "Disputes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "c5379df3-74d9-4897-8d49-3efb167f9ac2", "AQAAAAIAAYagAAAAEO05zvepo7s/4e9I65uSA+5H78mpnLRGzEpI70W+D1cwVMi+VrNjBw1b6j35YMje0A==", new DateTime(2025, 5, 4, 1, 47, 4, 49, DateTimeKind.Local).AddTicks(9439), "93ffaa90-aaa7-4d73-b982-11bd9049066b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isResolved",
                table: "Disputes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "76b2e7f8-fccf-4a30-b43d-c365fc8760b0", "AQAAAAIAAYagAAAAEAmtBSpnFdeSdxWu4YMXh3sGvx4apxKA8EcRmE0HVyIm6upIZjT/3AKZmRKnqv2M/Q==", new DateTime(2025, 5, 3, 22, 51, 25, 152, DateTimeKind.Local).AddTicks(7274), "e65e0397-a665-4003-9f44-35b59de61462" });
        }
    }
}
