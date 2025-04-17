using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class withdrawalandfunds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerWithdrawals");

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funds_Payments_Id",
                        column: x => x.Id,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funds_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Funds_freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawals_Payments_Id",
                        column: x => x.Id,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Withdrawals_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Withdrawals_freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "a80d2502-b1b4-43da-834c-f4b6df45dd0d", "AQAAAAIAAYagAAAAEJsp2cOjnPdqopuu3OxF0ACFc1SokWUlJa6ee3ljeFbK+Np5EQVDDHT5dmSJfKua9g==", new DateTime(2025, 4, 17, 20, 42, 46, 222, DateTimeKind.Local).AddTicks(8577), "abeec213-a3c6-4e30-8fb3-c66167e0cb3c" });

            migrationBuilder.CreateIndex(
                name: "IX_Funds_ClientId",
                table: "Funds",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Funds_FreelancerId",
                table: "Funds",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_ClientId",
                table: "Withdrawals",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawals_FreelancerId",
                table: "Withdrawals",
                column: "FreelancerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "Withdrawals");

            migrationBuilder.CreateTable(
                name: "FreelancerWithdrawals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FreelancerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerWithdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerWithdrawals_Payments_Id",
                        column: x => x.Id,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerWithdrawals_freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "ac6e8a57-4794-4137-ba62-26ce11475b36", "AQAAAAIAAYagAAAAEJIeOc2NawJLABISqmIHIzbbFVTcoYFSqdk5qFrPp4E+7hSZjZaVqRPEYdEVG4tNdA==", new DateTime(2025, 4, 17, 19, 36, 54, 554, DateTimeKind.Local).AddTicks(549), "f5bb7fc9-eb4c-4f54-bd60-1105fee1c8dd" });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerWithdrawals_FreelancerId",
                table: "FreelancerWithdrawals",
                column: "FreelancerId");
        }
    }
}
