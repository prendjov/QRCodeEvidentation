using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingModelsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureGroupCourse");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "LectureGroupCourse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    LectureGroupId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureGroupCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureGroupCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureGroupCourse_LectureGroup_LectureGroupId",
                        column: x => x.LectureGroupId,
                        principalTable: "LectureGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LectureGroupCourse_CourseId",
                table: "LectureGroupCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureGroupCourse_LectureGroupId",
                table: "LectureGroupCourse",
                column: "LectureGroupId");
        }
    }
}
