using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class UserConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConnections_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "d68dd655-561a-4add-9bfd-9c55fdf87bca", "AQAAAAIAAYagAAAAELUag73Ev0rjQmrEG5/ib8vhDkn4oPOCgdgjaNWUIvWN/S1XnbAcZwXyU1E9mwx/sw==", new DateTime(2025, 4, 13, 15, 52, 45, 686, DateTimeKind.Local).AddTicks(3498), "8528c7fd-3805-451d-90ec-813b8dba578b" });

            migrationBuilder.CreateIndex(
                name: "IX_UserConnections_UserId",
                table: "UserConnections",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConnections");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "e50f0f1b-9981-405e-9363-8f7dc19e9d9b", "AQAAAAIAAYagAAAAEIIv/lEAgGpc6q/LHadZu41nYliuoXZmvVXU+s1ho+u9DJ1RzmHj/CCaNcFXDkog/A==", new DateTime(2025, 4, 13, 0, 38, 58, 116, DateTimeKind.Local).AddTicks(8331), "12cc85b5-b054-4a34-a03c-7efcaac5ada2" });
        }
    }
}
