using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Chat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "e50f0f1b-9981-405e-9363-8f7dc19e9d9b", "AQAAAAIAAYagAAAAELH7G/AC071QdGNEn26c7fQB2nbyM9oTnicSVaivWXy5Z8UiQYXg9fHoUQGIv40MXw==", new DateTime(2025, 4, 13, 2, 17, 8, 418, DateTimeKind.Local).AddTicks(3673), "12cc85b5-b054-4a34-a03c-7efcaac5ada2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d6c68b5a-0ffd-4851-bdc2-cc2dacc2e3e0", "AQAAAAIAAYagAAAAEHZp7S1CBBAnjJmQFoSSQtaMt6pacMTrhUYjF9ho2EbqA3RBWxh29gMrzyEA+FbDKg==", new DateTime(2025, 4, 13, 1, 39, 5, 504, DateTimeKind.Local).AddTicks(2714), "c8486e60-a53e-4742-b76a-c11f49641865" });
        }
    }
}
