using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinabitAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHGuestMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "ErrorDescription",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "ErrorID",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "Guest",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "GuestType",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "InsBy",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "InsDate",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "LUB",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "LUD",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "LUN",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "rowguid",
                table: "tblHGuest");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "tblHGuest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblHGuest_PatientId",
                table: "tblHGuest",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblHGuest_tblCLPatients_PatientId",
                table: "tblHGuest",
                column: "PatientId",
                principalTable: "tblCLPatients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblHGuest_tblCLPatients_PatientId",
                table: "tblHGuest");

            migrationBuilder.DropIndex(
                name: "IX_tblHGuest_PatientId",
                table: "tblHGuest");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "tblHGuest");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblHGuest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrorDescription",
                table: "tblHGuest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ErrorID",
                table: "tblHGuest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Guest",
                table: "tblHGuest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuestType",
                table: "tblHGuest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsBy",
                table: "tblHGuest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsDate",
                table: "tblHGuest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LUB",
                table: "tblHGuest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LUD",
                table: "tblHGuest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LUN",
                table: "tblHGuest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "rowguid",
                table: "tblHGuest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
