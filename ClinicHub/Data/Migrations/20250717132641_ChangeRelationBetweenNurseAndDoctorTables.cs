using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationBetweenNurseAndDoctorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorNurseAssignments");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Nurses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_DoctorId",
                table: "Nurses",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Doctors_DoctorId",
                table: "Nurses",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Doctors_DoctorId",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_DoctorId",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Nurses");

            migrationBuilder.CreateTable(
                name: "DoctorNurseAssignments",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    NurseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorNurseAssignments", x => new { x.DoctorId, x.NurseId });
                    table.ForeignKey(
                        name: "FK_DoctorNurseAssignments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorNurseAssignments_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNurseAssignments_NurseId",
                table: "DoctorNurseAssignments",
                column: "NurseId");
        }
    }
}
