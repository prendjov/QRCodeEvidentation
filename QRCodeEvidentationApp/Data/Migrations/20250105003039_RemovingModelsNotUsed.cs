using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingModelsNotUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudyPrograms_StudyProgramCode",
                table: "Students");

            migrationBuilder.DropTable(
                name: "ProfessorDetails");

            migrationBuilder.DropTable(
                name: "StudyPrograms");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudyProgramCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OrderingRank",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Professors");

            migrationBuilder.AlterColumn<string>(
                name: "StudyProgramCode",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudyProgramCode",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "OrderingRank",
                table: "Professors",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfessorDetails",
                columns: table => new
                {
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: true),
                    CurrentTitleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorDetails", x => x.ProfessorId);
                    table.ForeignKey(
                        name: "FK_ProfessorDetails_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyPrograms",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPrograms", x => x.Code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudyProgramCode",
                table: "Students",
                column: "StudyProgramCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudyPrograms_StudyProgramCode",
                table: "Students",
                column: "StudyProgramCode",
                principalTable: "StudyPrograms",
                principalColumn: "Code");
        }
    }
}
