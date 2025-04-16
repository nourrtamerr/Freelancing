using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class ProjectIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Project",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Project");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96fa1d22-15fc-49ee-ae9d-a2d72dde0bfb", "AQAAAAIAAYagAAAAELwhapaH68Y4Ad/K3QzL34mltOxGFnh6mLdMVepJxCeMixT4hMjsL8/DkSUIL3PdOg==", "c974c1f2-7bb0-4d3e-a632-7836742415cf" });
        }
    }
}
