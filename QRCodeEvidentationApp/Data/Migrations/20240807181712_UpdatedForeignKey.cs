using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LectureGroupCourse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LectureGroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureGroupCourse");
        }
    }
}
