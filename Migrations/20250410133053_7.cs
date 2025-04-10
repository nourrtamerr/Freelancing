using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_AspNetUsers_FreelancerId",
                table: "certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_FreelancerId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_AspNetUsers_FreelancerId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Projects_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Milestones_MilestoneId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PortofolioProjects_AspNetUsers_FreelancerId",
                table: "PortofolioProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ClientId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_FreelancerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Subcategories_SubcategoryId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_FreelancerId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_AspNetUsers_FreelancerId",
                table: "UserSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptionPlanPayments_Payments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_MilestoneId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BidCurrentPrice",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BiddingEndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BiddingStartDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectType",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "maximumprice",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "minimumPrice",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MilestoneId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "SubscriptionPayments");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AspNetUsers",
                newName: "isDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_SubcategoryId",
                table: "Project",
                newName: "IX_Project_SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_FreelancerId",
                table: "Project",
                newName: "IX_Project_FreelancerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ClientId",
                table: "Project",
                newName: "IX_Project_ClientId");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "UserSubscriptionPlanPayments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Proposals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionPayments",
                table: "SubscriptionPayments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "biddingProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    minimumPrice = table.Column<int>(type: "int", nullable: false),
                    maximumprice = table.Column<int>(type: "int", nullable: false),
                    BidCurrentPrice = table.Column<int>(type: "int", nullable: false),
                    BiddingStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BiddingEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_biddingProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_biddingProjects_Project_Id",
                        column: x => x.Id,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clients_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixedPriceProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fixedPriceProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fixedPriceProjects_Project_Id",
                        column: x => x.Id,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "freelancers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freelancers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_freelancers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilestonePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MilestoneId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestonePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilestonePayments_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "firstname", "isDeleted", "lastname" },
                values: new object[] { "1", 0, "Admin City", "1af0411b-9e49-44fb-bafd-02e99f8ac7af", "Admin Country", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAELRbFCGqI4ZmKgGJCDPtBm3GLa9d1qvTceIRsol7WRzZb6pIu6ybbbOTvyaeIffGtw==", null, false, null, "d13a279b-7775-44ed-a6da-8c7a4ea5b980", false, "admin", "Admin", false, "User" });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "Id",
                value: "1");

            migrationBuilder.CreateIndex(
                name: "IX_MilestonePayments_MilestoneId",
                table: "MilestonePayments",
                column: "MilestoneId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_freelancers_FreelancerId",
                table: "certificates",
                column: "FreelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_freelancers_FreelancerId",
                table: "Educations",
                column: "FreelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_freelancers_FreelancerId",
                table: "Experiences",
                column: "FreelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Project_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PortofolioProjects_freelancers_FreelancerId",
                table: "PortofolioProjects",
                column: "FreelancerId",
                principalTable: "freelancers",
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
                name: "FK_Proposals_Project_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_freelancers_FreelancerId",
                table: "Proposals",
                column: "FreelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_freelancers_FreelancerId",
                table: "UserSkills",
                column: "FreelancerId",
                principalTable: "freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptionPlanPayments_SubscriptionPayments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments",
                column: "SubscriptionPaymentId",
                principalTable: "SubscriptionPayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_freelancers_FreelancerId",
                table: "certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_freelancers_FreelancerId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_freelancers_FreelancerId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_Project_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_PortofolioProjects_freelancers_FreelancerId",
                table: "PortofolioProjects");

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
                name: "FK_Proposals_Project_ProjectId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_freelancers_FreelancerId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_freelancers_FreelancerId",
                table: "UserSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptionPlanPayments_SubscriptionPayments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "biddingProjects");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "fixedPriceProjects");

            migrationBuilder.DropTable(
                name: "freelancers");

            migrationBuilder.DropTable(
                name: "MilestonePayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionPayments",
                table: "SubscriptionPayments");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "UserSubscriptionPlanPayments");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Experiences");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "SubscriptionPayments",
                newName: "Payments");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "AspNetUsers",
                newName: "IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Project_SubcategoryId",
                table: "Projects",
                newName: "IX_Projects_SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_FreelancerId",
                table: "Projects",
                newName: "IX_Projects_FreelancerId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ClientId",
                table: "Projects",
                newName: "IX_Projects_ClientId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BidCurrentPrice",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BiddingEndDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BiddingStartDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectType",
                table: "Projects",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "maximumprice",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "minimumPrice",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MilestoneId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Payments",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MilestoneId",
                table: "Payments",
                column: "MilestoneId",
                unique: true,
                filter: "[MilestoneId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_AspNetUsers_FreelancerId",
                table: "certificates",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_FreelancerId",
                table: "Educations",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_AspNetUsers_FreelancerId",
                table: "Experiences",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_Projects_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Milestones_MilestoneId",
                table: "Payments",
                column: "MilestoneId",
                principalTable: "Milestones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PortofolioProjects_AspNetUsers_FreelancerId",
                table: "PortofolioProjects",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ClientId",
                table: "Projects",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_FreelancerId",
                table: "Projects",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Subcategories_SubcategoryId",
                table: "Projects",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_FreelancerId",
                table: "Proposals",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_AspNetUsers_FreelancerId",
                table: "UserSkills",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptionPlanPayments_Payments_SubscriptionPaymentId",
                table: "UserSubscriptionPlanPayments",
                column: "SubscriptionPaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
