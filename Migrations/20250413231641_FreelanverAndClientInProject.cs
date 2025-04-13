using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class FreelanverAndClientInProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_biddingProjects_Project_Id",
                table: "biddingProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_fixedPriceProjects_Project_Id",
                table: "fixedPriceProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Project_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_ClientId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_FreelancerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Subcategories_SubcategoryId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_Project_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Project_ProjectId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "project");

            migrationBuilder.RenameIndex(
                name: "IX_Project_SubcategoryId",
                table: "project",
                newName: "IX_project_SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_FreelancerId",
                table: "project",
                newName: "IX_project_FreelancerId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ClientId",
                table: "project",
                newName: "IX_project_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project",
                table: "project",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "7ef81e21-4599-4ebb-a148-fc4c8a17df60", "AQAAAAIAAYagAAAAECAgyvQ4cm5XAvsHpKFujlY2cR4dKea6oghqeWIhfbpx0mtVE93CTuz7qyGSefOHmQ==", new DateTime(2025, 4, 14, 1, 16, 40, 633, DateTimeKind.Local).AddTicks(7372), "ff6f58b0-d592-43a5-ab7d-d08cbdbf041b" });

            migrationBuilder.AddForeignKey(
                name: "FK_biddingProjects_project_Id",
                table: "biddingProjects",
                column: "Id",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fixedPriceProjects_project_Id",
                table: "fixedPriceProjects",
                column: "Id",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_project_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_Subcategories_SubcategoryId",
                table: "project",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_clients_ClientId",
                table: "project",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_freelancers_FreelancerId",
                table: "project",
                column: "FreelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_project_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_project_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_biddingProjects_project_Id",
                table: "biddingProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_fixedPriceProjects_project_Id",
                table: "fixedPriceProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_project_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_project_Subcategories_SubcategoryId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_clients_ClientId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_freelancers_FreelancerId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_project_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_project_ProjectId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project",
                table: "project");

            migrationBuilder.RenameTable(
                name: "project",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_project_SubcategoryId",
                table: "Project",
                newName: "IX_Project_SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_project_FreelancerId",
                table: "Project",
                newName: "IX_Project_FreelancerId");

            migrationBuilder.RenameIndex(
                name: "IX_project_ClientId",
                table: "Project",
                newName: "IX_Project_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "278ddb38-e885-46ff-a5d5-4fe5950c743b", "AQAAAAIAAYagAAAAEIzeTWTfc2PBM2D3dKru7eD7m4P/Q36CYcTIU20hr+7q4L7i4hmNHBrbnPYvzDGboQ==", new DateTime(2025, 4, 13, 16, 46, 21, 217, DateTimeKind.Local).AddTicks(7392), "131ac5f3-ea70-425d-847d-8b68f8438fdc" });

            migrationBuilder.AddForeignKey(
                name: "FK_biddingProjects_Project_Id",
                table: "biddingProjects",
                column: "Id",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fixedPriceProjects_Project_Id",
                table: "fixedPriceProjects",
                column: "Id",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Project_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_ClientId",
                table: "Project",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_FreelancerId",
                table: "Project",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Subcategories_SubcategoryId",
                table: "Project",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_Project_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Project_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
