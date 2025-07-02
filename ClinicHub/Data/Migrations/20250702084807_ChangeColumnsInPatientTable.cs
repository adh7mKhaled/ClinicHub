using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnsInPatientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Patients",
                newName: "MobileNumber");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Patients",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Patients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Patients",
                newName: "Notes");
        }
    }
}
