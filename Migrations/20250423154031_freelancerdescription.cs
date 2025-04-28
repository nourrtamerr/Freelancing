using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class freelancerdescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "freelancers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "c8a044e4-b342-496c-be03-b27f5772703b", "AQAAAAIAAYagAAAAEJczzlGWLt3BbmlKZYAymXzujq1CRf1l0P1TFHqyLBn35h9d1GYKXTguQ+NmOB9DEA==", new DateTime(2025, 4, 23, 17, 40, 29, 509, DateTimeKind.Local).AddTicks(5300), "9be57ae2-82d5-4541-b4b2-4523e40e5f73" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "freelancers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "3c78089f-d7e4-4e05-bf9e-b82d1a1de373", "AQAAAAIAAYagAAAAECSjevHPE/SdOLxC1FKQQoktgnxJLPirsNQxI/Ey2tqsG0QAp2RuaRhsnykw+dbLkg==", new DateTime(2025, 4, 18, 0, 24, 25, 557, DateTimeKind.Local).AddTicks(5303), "defe2304-2681-468c-85d1-ac957d40273f" });
        }
    }
}
