using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMemberStatusColumnFromPatientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropColumn(
                name: "MemberStatus",
                table: "Patients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberStatus",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nurses_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nurses_AspNetUsers_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nurses_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_CreatedById",
                table: "Nurses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_DoctorId",
                table: "Nurses",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Email",
                table: "Nurses",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_LastUpdatedById",
                table: "Nurses",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_MobileNumber",
                table: "Nurses",
                column: "MobileNumber",
                unique: true);
        }
    }
}
