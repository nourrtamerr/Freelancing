using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class Addingisdeletedpropertytothecategoryandsubcategorymodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Subcategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0de9937a-02aa-4948-8838-b37af2f3fd63", "AQAAAAIAAYagAAAAEDKEfD98ZnqxnTHpn2WMwnUKvWKDUeMUubJbDT2iIrwpvbgXgtsNs1FSvEQ5nBPgbQ==", "88fc0fe8-29e8-4dac-b04b-62d6623a9fe7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1af0411b-9e49-44fb-bafd-02e99f8ac7af", "AQAAAAIAAYagAAAAELRbFCGqI4ZmKgGJCDPtBm3GLa9d1qvTceIRsol7WRzZb6pIu6ybbbOTvyaeIffGtw==", "d13a279b-7775-44ed-a6da-8c7a4ea5b980" });
        }
    }
}
