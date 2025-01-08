using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class LectureGroupAddedToLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LectureGroupId",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LectureGroupId",
                table: "Lectures",
                column: "LectureGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_LectureGroup_LectureGroupId",
                table: "Lectures",
                column: "LectureGroupId",
                principalTable: "LectureGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_LectureGroup_LectureGroupId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_LectureGroupId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "LectureGroupId",
                table: "Lectures");
        }
    }
}
