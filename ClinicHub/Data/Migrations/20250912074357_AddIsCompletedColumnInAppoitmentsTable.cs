using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCompletedColumnInAppoitmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Appointments");
        }
    }
}
