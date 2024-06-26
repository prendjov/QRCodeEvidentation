using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnneededClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_JoinedSubjects_JoinedSubjectId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_AssistantId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Students_StudentIndex1",
                table: "LectureAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureCourses_Courses_CourseId",
                table: "LectureCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureCourses_Lectures_LectureId",
                table: "LectureCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Rooms_RoomName",
                table: "Lectures");

            migrationBuilder.DropTable(
                name: "AuthUsers");

            migrationBuilder.DropTable(
                name: "CourseProfessorEvaluations");

            migrationBuilder.DropTable(
                name: "CourseStudentGroups");

            migrationBuilder.DropTable(
                name: "StudentSubjectEnrollments");

            migrationBuilder.DropTable(
                name: "StudyProgramSubjectProfessors");

            migrationBuilder.DropTable(
                name: "SubjectNameMappings");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "JoinedSubjects");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "StudyProgramSubjects");

            migrationBuilder.DropTable(
                name: "SubjectDetails");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AssistantId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Assistants",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Professors",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "JoinedSubjectId",
                table: "Courses",
                newName: "ProfessorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_JoinedSubjectId",
                table: "Courses",
                newName: "IX_Courses_ProfessorId1");

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "StudentCourses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessorId",
                table: "StudentCourses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorId",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LectureId",
                table: "LectureCourses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "LectureCourses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIndex1",
                table: "LectureAttendances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIndex",
                table: "LectureAttendances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LectureId",
                table: "LectureAttendances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "CourseAssistants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssistantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAssistants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseAssistants_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseAssistants_Professors_AssistantId",
                        column: x => x.AssistantId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CourseProfessors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProfessors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseProfessors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseProfessors_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_AssistantId",
                table: "StudentCourses",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_ProfessorId",
                table: "StudentCourses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssistants_AssistantId",
                table: "CourseAssistants",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssistants_CourseId",
                table: "CourseAssistants",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessors_CourseId",
                table: "CourseProfessors",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessors_ProfessorId",
                table: "CourseProfessors",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorId1",
                table: "Courses",
                column: "ProfessorId1",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Students_StudentIndex1",
                table: "LectureAttendances",
                column: "StudentIndex1",
                principalTable: "Students",
                principalColumn: "StudentIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureCourses_Courses_CourseId",
                table: "LectureCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureCourses_Lectures_LectureId",
                table: "LectureCourses",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Rooms_RoomName",
                table: "Lectures",
                column: "RoomName",
                principalTable: "Rooms",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Professors_AssistantId",
                table: "StudentCourses",
                column: "AssistantId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Professors_ProfessorId",
                table: "StudentCourses",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Students_StudentIndex1",
                table: "LectureAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureCourses_Courses_CourseId",
                table: "LectureCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureCourses_Lectures_LectureId",
                table: "LectureCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Rooms_RoomName",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Professors_AssistantId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Professors_ProfessorId",
                table: "StudentCourses");

            migrationBuilder.DropTable(
                name: "CourseAssistants");

            migrationBuilder.DropTable(
                name: "CourseProfessors");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_AssistantId",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_ProfessorId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "StudentCourses");

            migrationBuilder.RenameColumn(
                name: "ProfessorId1",
                table: "Courses",
                newName: "JoinedSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ProfessorId1",
                table: "Courses",
                newName: "IX_Courses_JoinedSubjectId");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorId",
                table: "Lectures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LectureId",
                table: "LectureCourses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "LectureCourses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentIndex1",
                table: "LectureAttendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentIndex",
                table: "LectureAttendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LectureId",
                table: "LectureAttendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assistants",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Professors",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseProfessorEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProfessorEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseProfessorEvaluations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseProfessorEvaluations_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    English = table.Column<bool>(type: "bit", nullable: true),
                    LastNameRegex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Programs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyYear = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Semesters_SemesterCode",
                        column: x => x.SemesterCode,
                        principalTable: "Semesters",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "SubjectNameMappings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectNameMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeeklyAuditoriumClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLabClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLecturesClasses = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudentGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    StudentGroupsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseStudentGroups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudentGroups_StudentGroups_StudentGroupsId",
                        column: x => x.StudentGroupsId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinedSubjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Codes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeeklyAuditoriumClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLabClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLecturesClasses = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinedSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubjectDetails",
                columns: table => new
                {
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccreditationYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityPoints = table.Column<short>(type: "smallint", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyOfSubjectDetailsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credits = table.Column<float>(type: "real", nullable: true),
                    Cycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultSemester = table.Column<short>(type: "smallint", nullable: true),
                    Dependencies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DependencyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamPoints = table.Column<short>(type: "smallint", nullable: true),
                    ExerciseHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsDescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeworkHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearningMethods = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placeholder = table.Column<bool>(type: "bit", nullable: true),
                    ProjectHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectPoints = table.Column<short>(type: "smallint", nullable: true),
                    QualityControl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelfLearningHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestsPoints = table.Column<short>(type: "smallint", nullable: true),
                    TotalHours = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDetails", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_SubjectDetails_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjectEnrollments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    JoinedSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SemesterCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentGroupId = table.Column<long>(type: "bigint", nullable: true),
                    StudentStudentIndex = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Assistants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvalidNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumEnrollments = table.Column<short>(type: "smallint", nullable: true),
                    Professors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_JoinedSubjects_JoinedSubjectId",
                        column: x => x.JoinedSubjectId,
                        principalTable: "JoinedSubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_Semesters_SemesterCode",
                        column: x => x.SemesterCode,
                        principalTable: "Semesters",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_Students_StudentStudentIndex",
                        column: x => x.StudentStudentIndex,
                        principalTable: "Students",
                        principalColumn: "StudentIndex",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjectEnrollments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyProgramSubjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudyProgramCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubjectDetailSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DependenciesOverride = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mandatory = table.Column<bool>(type: "bit", nullable: true),
                    Order = table.Column<double>(type: "float", nullable: true),
                    Semester = table.Column<short>(type: "smallint", nullable: true),
                    SubjectGroup = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyProgramSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyProgramSubjects_StudyPrograms_StudyProgramCode",
                        column: x => x.StudyProgramCode,
                        principalTable: "StudyPrograms",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_StudyProgramSubjects_SubjectDetails_SubjectDetailSubjectId",
                        column: x => x.SubjectDetailSubjectId,
                        principalTable: "SubjectDetails",
                        principalColumn: "SubjectId");
                    table.ForeignKey(
                        name: "FK_StudyProgramSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyProgramSubjectProfessors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudyProgramSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Order = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyProgramSubjectProfessors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyProgramSubjectProfessors_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyProgramSubjectProfessors_StudyProgramSubjects_StudyProgramSubjectId",
                        column: x => x.StudyProgramSubjectId,
                        principalTable: "StudyProgramSubjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AssistantId",
                table: "Courses",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessorEvaluations_CourseId",
                table: "CourseProfessorEvaluations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessorEvaluations_ProfessorId",
                table: "CourseProfessorEvaluations",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentGroups_CourseId",
                table: "CourseStudentGroups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentGroups_StudentGroupsId",
                table: "CourseStudentGroups",
                column: "StudentGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedSubjects_SubjectId",
                table: "JoinedSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_SemesterCode",
                table: "StudentGroups",
                column: "SemesterCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_CourseId",
                table: "StudentSubjectEnrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_JoinedSubjectId",
                table: "StudentSubjectEnrollments",
                column: "JoinedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_ProfessorId",
                table: "StudentSubjectEnrollments",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_SemesterCode",
                table: "StudentSubjectEnrollments",
                column: "SemesterCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_StudentGroupId",
                table: "StudentSubjectEnrollments",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_StudentStudentIndex",
                table: "StudentSubjectEnrollments",
                column: "StudentStudentIndex");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectEnrollments_SubjectId",
                table: "StudentSubjectEnrollments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProgramSubjectProfessors_ProfessorId",
                table: "StudyProgramSubjectProfessors",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProgramSubjectProfessors_StudyProgramSubjectId",
                table: "StudyProgramSubjectProfessors",
                column: "StudyProgramSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProgramSubjects_StudyProgramCode",
                table: "StudyProgramSubjects",
                column: "StudyProgramCode");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProgramSubjects_SubjectDetailSubjectId",
                table: "StudyProgramSubjects",
                column: "SubjectDetailSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProgramSubjects_SubjectId",
                table: "StudyProgramSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_ProfessorId",
                table: "TeacherSubjects",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_JoinedSubjects_JoinedSubjectId",
                table: "Courses",
                column: "JoinedSubjectId",
                principalTable: "JoinedSubjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_AssistantId",
                table: "Courses",
                column: "AssistantId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Lectures_LectureId",
                table: "LectureAttendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Students_StudentIndex1",
                table: "LectureAttendances",
                column: "StudentIndex1",
                principalTable: "Students",
                principalColumn: "StudentIndex",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureCourses_Courses_CourseId",
                table: "LectureCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureCourses_Lectures_LectureId",
                table: "LectureCourses",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Rooms_RoomName",
                table: "Lectures",
                column: "RoomName",
                principalTable: "Rooms",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
