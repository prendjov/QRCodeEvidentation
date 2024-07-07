using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ValidRegistrationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ValidRegistrationUntil",
                table: "Lectures",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidRegistrationUntil",
                table: "Lectures");
        }
    }
}
