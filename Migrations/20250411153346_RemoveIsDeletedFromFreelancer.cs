using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsDeletedFromFreelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "freelancers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68e0dec4-e6d6-48d3-ac78-df6ed959f1f0", "AQAAAAIAAYagAAAAEDvKER6qOy8suiofsPcQsmG8QzELogWubbwOCqqQbB4lm9EaA/VQHbycs4UfiNOyug==", "58ceeb36-b76a-4bda-bf45-7a5c7c627d35" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "freelancers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "005302f8-e0e2-4cfd-9ae5-3b4926e849dd", "AQAAAAIAAYagAAAAEG7yQjKZvBauJrnkeO0OYLxC+u/Z4/l2prBrM2ZA0H+x4Wdp21wC9Jyu/w1UhTDvEw==", "be9660f5-9268-4a4f-9904-da24a68209e7" });
        }
    }
}
