using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class subscriptionplansseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "bb6d772d-3c87-4913-a3ee-c5260f4fbcc6", "AQAAAAIAAYagAAAAEFfXqmlqsWUgz6C+F+Q+KslVRBKa8QBsYk0S5YYpDhWDyLTgrKtgyVGiCO28UHKEKA==", new DateTime(2025, 4, 17, 17, 1, 51, 13, DateTimeKind.Local).AddTicks(2095), "31fe4802-ee1d-49cb-8344-bdfbd6049c38" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Description", "DurationInDays", "Price", "TotalNumber", "isDeleted", "name" },
                values: new object[,]
                {
                    {  "Basic access, limited bids/applications.", 30, 0m, 6, false, "Starter" },
                    {  "More bids, priority support.", 60, 100m, 30, false, "Pro Freelancer" },
                    {  "Maximum exposure, profile boosts.", 90, 200m, 60, false, "Elite" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "84c466f7-cbbe-459f-b4b1-b3f5ba571512", "AQAAAAIAAYagAAAAEJuLw4YySZjoYH7wnsqTXxBoXl/rn1QQu8pNhr8qEW9MmMBJikWjkXxd4yuTlUxeEg==", new DateTime(2025, 4, 17, 1, 24, 17, 836, DateTimeKind.Local).AddTicks(9492), "02c2e1ea-dda4-4cb3-823d-9f78a4ebefde" });
        }
    }
}
