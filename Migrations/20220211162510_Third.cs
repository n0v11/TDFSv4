using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TDFSv4.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Founders",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2022, 2, 11, 19, 25, 8, 944, DateTimeKind.Local).AddTicks(5384));

            migrationBuilder.CreateIndex(
                name: "IX_Founders_ClientId",
                table: "Founders",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Founders_Clients_ClientId",
                table: "Founders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Founders_Clients_ClientId",
                table: "Founders");

            migrationBuilder.DropIndex(
                name: "IX_Founders_ClientId",
                table: "Founders");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Founders");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdateDate",
                value: new DateTime(2022, 2, 9, 18, 17, 4, 5, DateTimeKind.Local).AddTicks(5044));
        }
    }
}
