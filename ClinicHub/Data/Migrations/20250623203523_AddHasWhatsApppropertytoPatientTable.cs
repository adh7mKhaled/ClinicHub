using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHasWhatsApppropertytoPatientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasWhatsApp",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasWhatsApp",
                table: "Patients");
        }
    }
}
