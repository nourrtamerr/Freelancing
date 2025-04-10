using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class SuggestedMilestone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "suggestedMilestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ProposalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggestedMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_suggestedMilestones_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d71c26cf-06bc-4c3a-9ede-fc401b57b250", "AQAAAAIAAYagAAAAEI+40O+DkbSUIbLrWwVpYUhpUnpEsf8XlweSE3SK0jO8krtDSjsH8Y+VpIESiTBVjA==", "52fc2b80-8f48-430f-9027-51227c7290be" });

            migrationBuilder.CreateIndex(
                name: "IX_suggestedMilestones_ProposalId",
                table: "suggestedMilestones",
                column: "ProposalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suggestedMilestones");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f2efa0b-79d2-43f4-8e67-8c037a4a2ab5", "AQAAAAIAAYagAAAAEF/GOuMVRbf2mB36RdxJmCA3vnY0k/dl//yzxzXvoTBCFAz3tqkvKKQFsWBOPbyeRA==", "7d9b42ee-66f5-45ba-91a1-378cea922311" });
        }
    }
}
