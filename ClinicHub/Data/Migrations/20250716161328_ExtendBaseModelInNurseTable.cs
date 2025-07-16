using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicHub.Migrations
{
    /// <inheritdoc />
    public partial class ExtendBaseModelInNurseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Nurses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Nurses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Nurses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Nurses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Nurses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_CreatedById",
                table: "Nurses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_LastUpdatedById",
                table: "Nurses",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_AspNetUsers_CreatedById",
                table: "Nurses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_AspNetUsers_LastUpdatedById",
                table: "Nurses",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_AspNetUsers_CreatedById",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_AspNetUsers_LastUpdatedById",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_CreatedById",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_LastUpdatedById",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Nurses");
        }
    }
}
