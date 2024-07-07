using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Professor_n_Student_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssistants_Professors_AssistantId",
                table: "CourseAssistants");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseProfessors_Professors_ProfessorId",
                table: "CourseProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Students_StudentIndex1",
                table: "LectureAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureGroup_Professors_ProfessorId",
                table: "LectureGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorDetails_Professors_ProfessorId",
                table: "ProfessorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Professors_AssistantId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Professors_ProfessorId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentStudentIndex",
                table: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_StudentStudentIndex",
                table: "StudentCourses");

            migrationBuilder.RenameColumn(
                name: "StudentIndex1",
                table: "LectureAttendances",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureAttendances_StudentIndex1",
                table: "LectureAttendances",
                newName: "IX_LectureAttendances_StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIndex",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Students",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentStudentIndex",
                table: "StudentCourses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "StudentCourses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "OrderingRank",
                table: "AspNetUsers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssistants_AspNetUsers_AssistantId",
                table: "CourseAssistants",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProfessors_AspNetUsers_ProfessorId",
                table: "CourseProfessors",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_ProfessorId",
                table: "Courses",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_ProfessorId1",
                table: "Courses",
                column: "ProfessorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Students_StudentId",
                table: "LectureAttendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureGroup_AspNetUsers_ProfessorId",
                table: "LectureGroup",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_AspNetUsers_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorDetails_AspNetUsers_ProfessorId",
                table: "ProfessorDetails",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_AspNetUsers_AssistantId",
                table: "StudentCourses",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_AspNetUsers_ProfessorId",
                table: "StudentCourses",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssistants_AspNetUsers_AssistantId",
                table: "CourseAssistants");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseProfessors_AspNetUsers_ProfessorId",
                table: "CourseProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_ProfessorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_ProfessorId1",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureAttendances_Students_StudentId",
                table: "LectureAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureGroup_AspNetUsers_ProfessorId",
                table: "LectureGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_AspNetUsers_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorDetails_AspNetUsers_ProfessorId",
                table: "ProfessorDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_AspNetUsers_AssistantId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_AspNetUsers_ProfessorId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrderingRank",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "LectureAttendances",
                newName: "StudentIndex1");

            migrationBuilder.RenameIndex(
                name: "IX_LectureAttendances_StudentId",
                table: "LectureAttendances",
                newName: "IX_LectureAttendances_StudentIndex1");

            migrationBuilder.AlterColumn<string>(
                name: "StudentIndex",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StudentStudentIndex",
                table: "StudentCourses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentIndex");

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderingRank = table.Column<short>(type: "smallint", nullable: true),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentStudentIndex",
                table: "StudentCourses",
                column: "StudentStudentIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssistants_Professors_AssistantId",
                table: "CourseAssistants",
                column: "AssistantId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProfessors_Professors_ProfessorId",
                table: "CourseProfessors",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorId",
                table: "Courses",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorId1",
                table: "Courses",
                column: "ProfessorId1",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureAttendances_Students_StudentIndex1",
                table: "LectureAttendances",
                column: "StudentIndex1",
                principalTable: "Students",
                principalColumn: "StudentIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_LectureGroup_Professors_ProfessorId",
                table: "LectureGroup",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorDetails_Professors_ProfessorId",
                table: "ProfessorDetails",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentStudentIndex",
                table: "StudentCourses",
                column: "StudentStudentIndex",
                principalTable: "Students",
                principalColumn: "StudentIndex");
        }
    }
}
