using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class milestoneFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Milestones");

            migrationBuilder.CreateTable(
                name: "MilestoneFiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MilestoneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneFiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_MilestoneFiles_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "07727a9d-dfd7-4e0c-ae2d-57a12d271ca7", "AQAAAAIAAYagAAAAEICAUj8sXnjYuUzSqnlnJgLr6OUkxcaukyGZdKxP2g0D3DwYajwD0YegbR5gZYaqSA==", new DateTime(2025, 4, 16, 22, 7, 0, 729, DateTimeKind.Local).AddTicks(8737), "46722397-74d6-48ed-bb51-6133555b8830" });

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneFiles_MilestoneId",
                table: "MilestoneFiles",
                column: "MilestoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MilestoneFiles");

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Milestones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "937b019c-df78-4220-8077-bc7f20ede094", "AQAAAAIAAYagAAAAEHO/rxORT5p0pYsvakuohg6Qs1iySnVRTIpCjWcjxnWN/BBe5SZWt5dA79ufyacNdw==", new DateTime(2025, 4, 16, 19, 10, 50, 106, DateTimeKind.Local).AddTicks(5232), "076f1993-f8c6-4d32-b57f-70c3fe759958" });
        }
    }
}
