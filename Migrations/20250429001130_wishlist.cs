using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FreelancerWishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerWishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerWishlists_freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FreelancerWishlists_project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "project",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "cec641ed-0a83-4d1a-803b-94b397851929", "AQAAAAIAAYagAAAAEE8yituCwpOQea8vzXfidzRdLYiKw20/1H4KxZna1CY6zo67QO50dtgTIo5v0KqVGA==", new DateTime(2025, 4, 29, 2, 11, 27, 794, DateTimeKind.Local).AddTicks(3953), "10ae568e-5647-43ff-a028-1e2429ee3e34" });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerWishlists_FreelancerId",
                table: "FreelancerWishlists",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerWishlists_ProjectId",
                table: "FreelancerWishlists",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerWishlists");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "097a6efb-5b41-462a-9b45-0e6102fd2793", "AQAAAAIAAYagAAAAEAe7TOlV/qE4Xj+rR8lG4+X/geXAh4osZsPqkVxC3sunNlYxU0fe6xzDulcMh5o3/g==", new DateTime(2025, 4, 27, 3, 5, 5, 899, DateTimeKind.Local).AddTicks(4044), "8ca6ffac-e23c-44c2-8ede-4cabe2559ba9" });
        }
    }
}
