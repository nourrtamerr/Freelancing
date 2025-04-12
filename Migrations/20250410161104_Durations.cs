using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Durations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuggestedDuration",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Project",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ExpectedDuration",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f2efa0b-79d2-43f4-8e67-8c037a4a2ab5", "AQAAAAIAAYagAAAAEF/GOuMVRbf2mB36RdxJmCA3vnY0k/dl//yzxzXvoTBCFAz3tqkvKKQFsWBOPbyeRA==", "7d9b42ee-66f5-45ba-91a1-378cea922311" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuggestedDuration",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ExpectedDuration",
                table: "Project");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d966e86-0b62-4e6e-8f4a-ada6fd3b2d0e", "AQAAAAIAAYagAAAAEFXgzcluy1ZPXv+JW7Ja52XPxydp89Mf07M8pysRzHEnykH+8J3dyWCGrThziqc9aA==", "2d0ccdda-ef47-42f6-b2a8-d52e2bed76c6" });
        }
    }
}
