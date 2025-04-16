using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class withdrawal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptionPlanPayments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments");

            migrationBuilder.CreateTable(
                name: "FreelancerWithdrawals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerWithdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerWithdrawals_freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "38a6ad05-63a4-4e03-bf40-38269c5bd32e", "AQAAAAIAAYagAAAAEDED5elCyRYnReCNMSbUgYU3tx7SPai/YiXEqWVKk896verFMM/pRoO8sUrxDCKRIQ==", new DateTime(2025, 4, 16, 23, 34, 34, 118, DateTimeKind.Local).AddTicks(1687), "00350cc0-60a7-4c3b-b34e-c4cb019b36e4" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptionPlanPayments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments",
                column: "SubscriptionPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerWithdrawals_FreelancerId",
                table: "FreelancerWithdrawals",
                column: "FreelancerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerWithdrawals");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptionPlanPayments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "07727a9d-dfd7-4e0c-ae2d-57a12d271ca7", "AQAAAAIAAYagAAAAEICAUj8sXnjYuUzSqnlnJgLr6OUkxcaukyGZdKxP2g0D3DwYajwD0YegbR5gZYaqSA==", new DateTime(2025, 4, 16, 22, 7, 0, 729, DateTimeKind.Local).AddTicks(8737), "46722397-74d6-48ed-bb51-6133555b8830" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptionPlanPayments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments",
                column: "SubscriptionPaymentId");
        }
    }
}
