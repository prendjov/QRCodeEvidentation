using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUnnecesaryModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureCourses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LectureCourses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    LectureId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LectureCourses_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LectureCourses_CourseId",
                table: "LectureCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureCourses_LectureId",
                table: "LectureCourses",
                column: "LectureId");
        }
    }
}
