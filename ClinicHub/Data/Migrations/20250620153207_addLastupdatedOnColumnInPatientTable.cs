using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class addLastupdatedOnColumnInPatientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Patients",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Patients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Patients",
                newName: "RegistrationDate");
        }
    }
}
