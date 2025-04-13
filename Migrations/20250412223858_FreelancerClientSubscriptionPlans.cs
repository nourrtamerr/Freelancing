using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class FreelancerClientSubscriptionPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "SubscriptionPlans");

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "SubscriptionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalNumber",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemainingNumberOfBids",
                table: "freelancers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "subscriptionPlanId",
                table: "freelancers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemainingNumberOfProjects",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "subscriptionPlanId",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "6c32f321-6714-47ef-a439-f0cb8f88250e", "AQAAAAIAAYagAAAAEIIv/lEAgGpc6q/LHadZu41nYliuoXZmvVXU+s1ho+u9DJ1RzmHj/CCaNcFXDkog/A==", new DateTime(2025, 4, 13, 0, 38, 58, 116, DateTimeKind.Local).AddTicks(8331), "e88bd599-9368-4e44-8ad0-18acb0a9a742" });

            migrationBuilder.CreateIndex(
                name: "IX_freelancers_subscriptionPlanId",
                table: "freelancers",
                column: "subscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_clients_subscriptionPlanId",
                table: "clients",
                column: "subscriptionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_SubscriptionPlans_subscriptionPlanId",
                table: "clients",
                column: "subscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_freelancers_SubscriptionPlans_subscriptionPlanId",
                table: "freelancers",
                column: "subscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_SubscriptionPlans_subscriptionPlanId",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_freelancers_SubscriptionPlans_subscriptionPlanId",
                table: "freelancers");

            migrationBuilder.DropIndex(
                name: "IX_freelancers_subscriptionPlanId",
                table: "freelancers");

            migrationBuilder.DropIndex(
                name: "IX_clients_subscriptionPlanId",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "TotalNumber",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "RemainingNumberOfBids",
                table: "freelancers");

            migrationBuilder.DropColumn(
                name: "subscriptionPlanId",
                table: "freelancers");

            migrationBuilder.DropColumn(
                name: "RemainingNumberOfProjects",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "subscriptionPlanId",
                table: "clients");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d82b0b08-3353-4578-a490-acd103535c75", "AQAAAAIAAYagAAAAEDQYK+jkLGS4bSkfLC00spqm62tQxt0f0y8G4g9KW6qlhccaHqLbRloXofGPjPlXQQ==", new DateTime(2025, 4, 12, 21, 42, 6, 459, DateTimeKind.Local).AddTicks(7899), "a16f2f61-d38c-437f-84f8-e80e19018fa3" });
        }
    }
}
