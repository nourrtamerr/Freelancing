using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Addingisdeletedpropertytotheeducationmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Educations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ede74c3-bcb4-4606-b109-7627c3f05437", "AQAAAAIAAYagAAAAEC/sN11fxkQIWQO5bV08T7GiQm5uL3MLTz9FJZl4MfvJ/97CkTS215TkfoWV43yCIg==", "774ac722-e9d4-45bd-999e-b5b9bb358c06" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Educations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0de9937a-02aa-4948-8838-b37af2f3fd63", "AQAAAAIAAYagAAAAEDKEfD98ZnqxnTHpn2WMwnUKvWKDUeMUubJbDT2iIrwpvbgXgtsNs1FSvEQ5nBPgbQ==", "88fc0fe8-29e8-4dac-b04b-62d6623a9fe7" });
        }
    }
}
