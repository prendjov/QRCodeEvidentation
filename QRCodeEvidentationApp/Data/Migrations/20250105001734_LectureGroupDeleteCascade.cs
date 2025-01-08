using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class LectureGroupDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_LectureGroup_LectureGroupId",
                table: "Lectures");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_LectureGroup_LectureGroupId",
                table: "Lectures",
                column: "LectureGroupId",
                principalTable: "LectureGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_LectureGroup_LectureGroupId",
                table: "Lectures");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_LectureGroup_LectureGroupId",
                table: "Lectures",
                column: "LectureGroupId",
                principalTable: "LectureGroup",
                principalColumn: "Id");
        }
    }
}
