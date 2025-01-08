using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureLectureAttendanceCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }
    }
}
