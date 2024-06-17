using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PrescriptionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfternoonDose",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "EveningDose",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "MorningDose",
                table: "Prescriptions",
                newName: "Dose");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimesTaken",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimesPerDay",
                table: "Prescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimesTaken",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimesPerDay",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Dose",
                table: "Prescriptions",
                newName: "MorningDose");

            migrationBuilder.AddColumn<double>(
                name: "AfternoonDose",
                table: "Prescriptions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EveningDose",
                table: "Prescriptions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
