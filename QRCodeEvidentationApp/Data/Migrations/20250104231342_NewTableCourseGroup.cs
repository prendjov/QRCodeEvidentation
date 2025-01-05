using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTableCourseGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLectureGroup_Courses_CoursesId",
                table: "CourseLectureGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseLectureGroup_LectureGroup_LectureGroupsId",
                table: "CourseLectureGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseLectureGroup",
                table: "CourseLectureGroup");

            migrationBuilder.DropIndex(
                name: "IX_CourseLectureGroup_LectureGroupsId",
                table: "CourseLectureGroup");

            migrationBuilder.RenameTable(
                name: "CourseLectureGroup",
                newName: "CustomCourseLectureGroupTable");

            migrationBuilder.RenameColumn(
                name: "LectureGroupsId",
                table: "CustomCourseLectureGroupTable",
                newName: "LectureGroupId");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "CustomCourseLectureGroupTable",
                newName: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomCourseLectureGroupTable",
                table: "CustomCourseLectureGroupTable",
                columns: new[] { "LectureGroupId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomCourseLectureGroupTable_CourseId",
                table: "CustomCourseLectureGroupTable",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCourseLectureGroupTable_Courses_CourseId",
                table: "CustomCourseLectureGroupTable",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCourseLectureGroupTable_LectureGroup_LectureGroupId",
                table: "CustomCourseLectureGroupTable",
                column: "LectureGroupId",
                principalTable: "LectureGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomCourseLectureGroupTable_Courses_CourseId",
                table: "CustomCourseLectureGroupTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomCourseLectureGroupTable_LectureGroup_LectureGroupId",
                table: "CustomCourseLectureGroupTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomCourseLectureGroupTable",
                table: "CustomCourseLectureGroupTable");

            migrationBuilder.DropIndex(
                name: "IX_CustomCourseLectureGroupTable_CourseId",
                table: "CustomCourseLectureGroupTable");

            migrationBuilder.RenameTable(
                name: "CustomCourseLectureGroupTable",
                newName: "CourseLectureGroup");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseLectureGroup",
                newName: "CoursesId");

            migrationBuilder.RenameColumn(
                name: "LectureGroupId",
                table: "CourseLectureGroup",
                newName: "LectureGroupsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseLectureGroup",
                table: "CourseLectureGroup",
                columns: new[] { "CoursesId", "LectureGroupsId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLectureGroup_LectureGroupsId",
                table: "CourseLectureGroup",
                column: "LectureGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLectureGroup_Courses_CoursesId",
                table: "CourseLectureGroup",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLectureGroup_LectureGroup_LectureGroupsId",
                table: "CourseLectureGroup",
                column: "LectureGroupsId",
                principalTable: "LectureGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
