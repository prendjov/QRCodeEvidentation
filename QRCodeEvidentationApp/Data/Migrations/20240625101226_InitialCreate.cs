using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderingRank = table.Column<short>(type: "smallint", nullable: true),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<long>(type: "bigint", nullable: true),
                    EquipmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EnrollmentStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EnrollmentEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Code);
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeeklyAuditoriumClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLabClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLecturesClasses = table.Column<int>(type: "int", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorDetails",
                columns: table => new
                {
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order = table.Column<double>(type: "float", nullable: true),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentTitleId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_Rooms_RoomName",
                        column: x => x.RoomName,
                        principalTable: "Rooms",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyYear = table.Column<short>(type: "smallint", nullable: true),
                    LastNameRegex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Programs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    English = table.Column<bool>(type: "bit", nullable: true)
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
                name: "Students",
                columns: table => new
                {
                    StudentIndex = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyProgramCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentIndex);
                    table.ForeignKey(
                        name: "FK_Students_StudyPrograms_StudyProgramCode",
                        column: x => x.StudyProgramCode,
                        principalTable: "StudyPrograms",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "JoinedSubjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WeeklyLecturesClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyAuditoriumClasses = table.Column<int>(type: "int", nullable: true),
                    WeeklyLabClasses = table.Column<int>(type: "int", nullable: true),
                    Cycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    DefaultSemester = table.Column<short>(type: "smallint", nullable: true),
                    AccreditationYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityPoints = table.Column<short>(type: "smallint", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyOfSubjectDetailsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credits = table.Column<float>(type: "real", nullable: true),
                    Cycle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dependencies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamPoints = table.Column<short>(type: "smallint", nullable: true),
                    ExerciseHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsDescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeworkHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearningMethods = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectPoints = table.Column<short>(type: "smallint", nullable: true),
                    SelfLearningHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestsPoints = table.Column<short>(type: "smallint", nullable: true),
                    TotalHours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualityControl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placeholder = table.Column<bool>(type: "bit", nullable: true),
                    DependencyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "LectureAttendances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LectureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentIndex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentIndex1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EvidentedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureAttendances_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureAttendances_Students_StudentIndex1",
                        column: x => x.StudentIndex1,
                        principalTable: "Students",
                        principalColumn: "StudentIndex",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyYear = table.Column<short>(type: "smallint", nullable: true),
                    LastNameRegex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JoinedSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssistantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NumberOfFirstEnrollments = table.Column<int>(type: "int", nullable: true),
                    NumberOfReEnrollments = table.Column<int>(type: "int", nullable: true),
                    GroupPortion = table.Column<float>(type: "real", nullable: true),
                    Professors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assistants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groups = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    English = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_JoinedSubjects_JoinedSubjectId",
                        column: x => x.JoinedSubjectId,
                        principalTable: "JoinedSubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Professors_AssistantId",
                        column: x => x.AssistantId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Semesters_SemesterCode",
                        column: x => x.SemesterCode,
                        principalTable: "Semesters",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "StudyProgramSubjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Mandatory = table.Column<bool>(type: "bit", nullable: true),
                    Semester = table.Column<short>(type: "smallint", nullable: true),
                    Order = table.Column<double>(type: "float", nullable: true),
                    StudyProgramCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DependenciesOverride = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectDetailSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                name: "CourseProfessorEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<short>(type: "smallint", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "LectureCourses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LectureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureCourses_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentStudentIndex = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentStudentIndex",
                        column: x => x.StudentStudentIndex,
                        principalTable: "Students",
                        principalColumn: "StudentIndex");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjectEnrollments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentStudentIndex = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: true),
                    InvalidNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumEnrollments = table.Column<short>(type: "smallint", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    JoinedSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Professors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assistants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    StudentGroupId = table.Column<long>(type: "bigint", nullable: true)
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
                name: "StudyProgramSubjectProfessors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudyProgramSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                name: "IX_CourseProfessorEvaluations_CourseId",
                table: "CourseProfessorEvaluations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessorEvaluations_ProfessorId",
                table: "CourseProfessorEvaluations",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AssistantId",
                table: "Courses",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_JoinedSubjectId",
                table: "Courses",
                column: "JoinedSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SemesterCode",
                table: "Courses",
                column: "SemesterCode");

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
                name: "IX_LectureAttendances_LectureId",
                table: "LectureAttendances",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureAttendances_StudentIndex1",
                table: "LectureAttendances",
                column: "StudentIndex1");

            migrationBuilder.CreateIndex(
                name: "IX_LectureCourses_CourseId",
                table: "LectureCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureCourses_LectureId",
                table: "LectureCourses",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ProfessorId",
                table: "Lectures",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_RoomName",
                table: "Lectures",
                column: "RoomName");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentStudentIndex",
                table: "StudentCourses",
                column: "StudentStudentIndex");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_SemesterCode",
                table: "StudentGroups",
                column: "SemesterCode");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudyProgramCode",
                table: "Students",
                column: "StudyProgramCode");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUsers");

            migrationBuilder.DropTable(
                name: "CourseProfessorEvaluations");

            migrationBuilder.DropTable(
                name: "CourseStudentGroups");

            migrationBuilder.DropTable(
                name: "LectureAttendances");

            migrationBuilder.DropTable(
                name: "LectureCourses");

            migrationBuilder.DropTable(
                name: "ProfessorDetails");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "StudentSubjectEnrollments");

            migrationBuilder.DropTable(
                name: "StudyProgramSubjectProfessors");

            migrationBuilder.DropTable(
                name: "SubjectNameMappings");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "StudyProgramSubjects");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "JoinedSubjects");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "StudyPrograms");

            migrationBuilder.DropTable(
                name: "SubjectDetails");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
