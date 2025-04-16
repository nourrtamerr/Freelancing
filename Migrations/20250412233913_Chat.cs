using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Chat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d6c68b5a-0ffd-4851-bdc2-cc2dacc2e3e0", "AQAAAAIAAYagAAAAEHZp7S1CBBAnjJmQFoSSQtaMt6pacMTrhUYjF9ho2EbqA3RBWxh29gMrzyEA+FbDKg==", new DateTime(2025, 4, 13, 1, 39, 5, 504, DateTimeKind.Local).AddTicks(2714), "c8486e60-a53e-4742-b76a-c11f49641865" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d82b0b08-3353-4578-a490-acd103535c75", "AQAAAAIAAYagAAAAEDQYK+jkLGS4bSkfLC00spqm62tQxt0f0y8G4g9KW6qlhccaHqLbRloXofGPjPlXQQ==", new DateTime(2025, 4, 12, 21, 42, 6, 459, DateTimeKind.Local).AddTicks(7899), "a16f2f61-d38c-437f-84f8-e80e19018fa3" });
        }
    }
}
