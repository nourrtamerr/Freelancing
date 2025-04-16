using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ahmedwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_project_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_project_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "fixedPrice",
                table: "fixedPriceProjects");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "fixedPriceProjects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "937b019c-df78-4220-8077-bc7f20ede094", "AQAAAAIAAYagAAAAEHO/rxORT5p0pYsvakuohg6Qs1iySnVRTIpCjWcjxnWN/BBe5SZWt5dA79ufyacNdw==", new DateTime(2025, 4, 16, 19, 10, 50, 106, DateTimeKind.Local).AddTicks(5232), "076f1993-f8c6-4d32-b57f-70c3fe759958" });

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_project_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_project_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_project_ProjectId",
                table: "Milestones");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_project_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "fixedPriceProjects");

            migrationBuilder.AddColumn<int>(
                name: "fixedPrice",
                table: "fixedPriceProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "28a59b3e-f1f4-4a0b-a9cd-0ffe74c1871a", "AQAAAAIAAYagAAAAECglvTv4PUAZF+DgCyRK47Hdousm13+a46KzkktifTl3Sh2GElkdYjF/KAigaYdckg==", new DateTime(2025, 4, 16, 0, 35, 7, 415, DateTimeKind.Local).AddTicks(9801), "42fc2d83-4b7c-40db-a8c6-51dc7fb24716" });

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_project_ProjectId",
                table: "Milestones",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_project_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId",
                principalTable: "project",
                principalColumn: "Id");
        }
    }
}
