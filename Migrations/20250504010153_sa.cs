using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freelancing.Migrations
{
    /// <inheritdoc />
    public partial class sa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "01636bf3-d772-492f-956e-caad6312a329", "AQAAAAIAAYagAAAAEC1lby1x1+a+xoFOrVnjNqiJZZWCijYrm2V+vzRtvmcSF0xRK+NrbqD3/liceO3Ppw==", new DateTime(2025, 5, 4, 3, 1, 50, 618, DateTimeKind.Local).AddTicks(8704), "cbf0160e-2425-4138-8e88-c6a07d6cdbd5" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProposalPayment_ProposalId",
                table: "ClientProposalPayment",
                column: "ProposalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientProposalPayment_Proposals_ProposalId",
                table: "ClientProposalPayment",
                column: "ProposalId",
                principalTable: "Proposals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientProposalPayment_Proposals_ProposalId",
                table: "ClientProposalPayment");

            migrationBuilder.DropIndex(
                name: "IX_ClientProposalPayment_ProposalId",
                table: "ClientProposalPayment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpiryDate", "SecurityStamp" },
                values: new object[] { "c5379df3-74d9-4897-8d49-3efb167f9ac2", "AQAAAAIAAYagAAAAEO05zvepo7s/4e9I65uSA+5H78mpnLRGzEpI70W+D1cwVMi+VrNjBw1b6j35YMje0A==", new DateTime(2025, 5, 4, 1, 47, 4, 49, DateTimeKind.Local).AddTicks(9439), "93ffaa90-aaa7-4d73-b982-11bd9049066b" });
        }
    }
}
