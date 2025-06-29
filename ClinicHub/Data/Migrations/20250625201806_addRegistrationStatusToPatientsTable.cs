using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRegistrationStatusToPatientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "RegistrationStatus",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationStatus",
                table: "Patients");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
