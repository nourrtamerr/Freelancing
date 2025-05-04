using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class clientproposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentPrediction",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "SentimentProbability",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "Disputes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MilestoneId = table.Column<int>(type: "int", nullable: false),
                    Complaint = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disputes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Disputes_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "de232101-dee3-4e2a-ad74-e18ff84349ad", "AQAAAAIAAYagAAAAEIQntn7hUwvb1iaPww4ai3VgRoljjkWfHs6sm6dzlC0H3E3QlGahAf9tD4Dw+wVsbg==", new DateTime(2025, 5, 4, 4, 4, 29, 81, DateTimeKind.Local).AddTicks(4776), "d8793873-7dbd-4334-bc17-07cd81e10582" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProposalPayment_ProposalId",
                table: "ClientProposalPayment",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Disputes_MilestoneId",
                table: "Disputes",
                column: "MilestoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientProposalPayment_Proposals_ProposalId",
                table: "ClientProposalPayment",
                column: "ProposalId",
                principalTable: "Proposals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientProposalPayment_Proposals_ProposalId",
                table: "ClientProposalPayment");

            migrationBuilder.DropTable(
                name: "Disputes");

            migrationBuilder.DropIndex(
                name: "IX_ClientProposalPayment_ProposalId",
                table: "ClientProposalPayment");

            migrationBuilder.AddColumn<string>(
                name: "SentimentPrediction",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SentimentProbability",
                table: "Reviews",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "1fb71671-9148-428a-a9de-65e8131e197e", "AQAAAAIAAYagAAAAELsEymShHRhtvoe/eGEgoBHkKfmwyuvixSsczIj7rgvqqrJ6cjSY9Us59jBqRk6mpg==", new DateTime(2025, 5, 3, 23, 50, 3, 783, DateTimeKind.Local).AddTicks(6116), "06b3135f-299c-4f39-a83f-2af5cf4ccffd" });
        }
    }
}
