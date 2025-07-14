using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseModelPropertiesToSpecialtyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Specialties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Specialties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Specialties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Specialties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Specialties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_CreatedById",
                table: "Specialties",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_LastUpdatedById",
                table: "Specialties",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_AspNetUsers_CreatedById",
                table: "Specialties",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_AspNetUsers_LastUpdatedById",
                table: "Specialties",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_AspNetUsers_CreatedById",
                table: "Specialties");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_AspNetUsers_LastUpdatedById",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_CreatedById",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_LastUpdatedById",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Specialties");
        }
    }
}
