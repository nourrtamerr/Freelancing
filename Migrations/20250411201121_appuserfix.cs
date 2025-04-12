using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class appuserfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Languages",
                table: "freelancers");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AccountCreationDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "FreelancerLanguage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    freelancerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerLanguage", x => x.id);
                    table.ForeignKey(
                        name: "FK_FreelancerLanguage_freelancers_freelancerId",
                        column: x => x.freelancerId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "AccountCreationDate", "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateOnly(1, 1, 1), "588d4854-46a2-4df0-9a6a-05f2f9128aa5", new DateOnly(1, 1, 1), "AQAAAAIAAYagAAAAELu7d+owkhv5aiAAng4Qxnfl448tKTH9ylXD/0aEowd1H+hJZhZFDmB+VRShFivtvQ==", "4967d530-9b1b-4b76-820d-e30431c0b212" });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerLanguage_freelancerId",
                table: "FreelancerLanguage",
                column: "freelancerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerLanguage");

            migrationBuilder.DropColumn(
                name: "AccountCreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "freelancers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96fa1d22-15fc-49ee-ae9d-a2d72dde0bfb", "AQAAAAIAAYagAAAAELwhapaH68Y4Ad/K3QzL34mltOxGFnh6mLdMVepJxCeMixT4hMjsL8/DkSUIL3PdOg==", "c974c1f2-7bb0-4d3e-a632-7836742415cf" });
        }
    }
}
