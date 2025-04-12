using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedPropertyAndChangeTheTypeOfFreelancerLanguagesfromfreelancerintoFreelancerLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerLanguage_freelancers_freelancerId",
                table: "FreelancerLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerLanguage",
                table: "FreelancerLanguage");

            migrationBuilder.RenameTable(
                name: "FreelancerLanguage",
                newName: "freelancerLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_FreelancerLanguage_freelancerId",
                table: "freelancerLanguages",
                newName: "IX_freelancerLanguages_freelancerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "freelancerLanguages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_freelancerLanguages",
                table: "freelancerLanguages",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d82b0b08-3353-4578-a490-acd103535c75", "AQAAAAIAAYagAAAAEDQYK+jkLGS4bSkfLC00spqm62tQxt0f0y8G4g9KW6qlhccaHqLbRloXofGPjPlXQQ==", new DateTime(2025, 4, 12, 21, 42, 6, 459, DateTimeKind.Local).AddTicks(7899), "a16f2f61-d38c-437f-84f8-e80e19018fa3" });

            migrationBuilder.AddForeignKey(
                name: "FK_freelancerLanguages_freelancers_freelancerId",
                table: "freelancerLanguages",
                column: "freelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_freelancerLanguages_freelancers_freelancerId",
                table: "freelancerLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_freelancerLanguages",
                table: "freelancerLanguages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "freelancerLanguages");

            migrationBuilder.RenameTable(
                name: "freelancerLanguages",
                newName: "FreelancerLanguage");

            migrationBuilder.RenameIndex(
                name: "IX_freelancerLanguages_freelancerId",
                table: "FreelancerLanguage",
                newName: "IX_FreelancerLanguage_freelancerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerLanguage",
                table: "FreelancerLanguage",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "b0c485c4-0d15-48f6-b4ed-bba6565370f0", "AQAAAAIAAYagAAAAEIwtusojaPCcv1JIdszQMS5mfLtu2QSOV6xqSKExp0zKN81y94aP9hc3ODoV4ZFrSw==", new DateTime(2025, 4, 12, 18, 47, 53, 940, DateTimeKind.Local).AddTicks(3813), "210eec2c-9760-4a37-9e38-12ad834de519" });

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerLanguage_freelancers_freelancerId",
                table: "FreelancerLanguage",
                column: "freelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");
        }
    }
}
