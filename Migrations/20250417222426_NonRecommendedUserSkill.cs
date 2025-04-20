using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class NonRecommendedUserSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nonRecommendedUserSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nonRecommendedUserSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FreelancerNonRecommendedUserSkill",
                columns: table => new
                {
                    FreelancersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NonRecommendedUserSkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerNonRecommendedUserSkill", x => new { x.FreelancersId, x.NonRecommendedUserSkillsId });
                    table.ForeignKey(
                        name: "FK_FreelancerNonRecommendedUserSkill_freelancers_FreelancersId",
                        column: x => x.FreelancersId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FreelancerNonRecommendedUserSkill_nonRecommendedUserSkills_NonRecommendedUserSkillsId",
                        column: x => x.NonRecommendedUserSkillsId,
                        principalTable: "nonRecommendedUserSkills",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "3c78089f-d7e4-4e05-bf9e-b82d1a1de373", "AQAAAAIAAYagAAAAECSjevHPE/SdOLxC1FKQQoktgnxJLPirsNQxI/Ey2tqsG0QAp2RuaRhsnykw+dbLkg==", new DateTime(2025, 4, 18, 0, 24, 25, 557, DateTimeKind.Local).AddTicks(5303), "defe2304-2681-468c-85d1-ac957d40273f" });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerNonRecommendedUserSkill_NonRecommendedUserSkillsId",
                table: "FreelancerNonRecommendedUserSkill",
                column: "NonRecommendedUserSkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerNonRecommendedUserSkill");

            migrationBuilder.DropTable(
                name: "nonRecommendedUserSkills");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "a80d2502-b1b4-43da-834c-f4b6df45dd0d", "AQAAAAIAAYagAAAAEJsp2cOjnPdqopuu3OxF0ACFc1SokWUlJa6ee3ljeFbK+Np5EQVDDHT5dmSJfKua9g==", new DateTime(2025, 4, 17, 20, 42, 46, 222, DateTimeKind.Local).AddTicks(8577), "abeec213-a3c6-4e30-8fb3-c66167e0cb3c" });
        }
    }
}
