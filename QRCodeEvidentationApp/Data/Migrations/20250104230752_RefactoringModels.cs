using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_LectureGroup_LectureGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LectureGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LectureGroupId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseLectureGroup",
                columns: table => new
                {
                    CoursesId = table.Column<long>(type: "bigint", nullable: false),
                    LectureGroupsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLectureGroup", x => new { x.CoursesId, x.LectureGroupsId });
                    table.ForeignKey(
                        name: "FK_CourseLectureGroup_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseLectureGroup_LectureGroup_LectureGroupsId",
                        column: x => x.LectureGroupsId,
                        principalTable: "LectureGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLectureGroup_LectureGroupsId",
                table: "CourseLectureGroup",
                column: "LectureGroupsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLectureGroup");

            migrationBuilder.AddColumn<string>(
                name: "LectureGroupId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LectureGroupId",
                table: "Courses",
                column: "LectureGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_LectureGroup_LectureGroupId",
                table: "Courses",
                column: "LectureGroupId",
                principalTable: "LectureGroup",
                principalColumn: "Id");
        }
    }
}
