﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QRCodeEvidentationApp.Data;

#nullable disable

namespace QRCodeEvidentationApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool?>("English")
                        .HasColumnType("bit");

                    b.Property<float?>("GroupPortion")
                        .HasColumnType("real");

                    b.Property<string>("Groups")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastNameRegex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfFirstEnrollments")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfReEnrollments")
                        .HasColumnType("int");

                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProfessorId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SemesterCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<short?>("StudyYear")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("ProfessorId1");

                    b.HasIndex("SemesterCode");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.CourseAssistant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssistantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("CourseId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AssistantId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseAssistants");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.CourseProfessor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("CourseProfessors");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Lecture", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ValidRegistrationUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("RoomName");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.LectureAttendance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EvidentedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LectureId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentIndex")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.HasIndex("StudentId");

                    b.ToTable("LectureAttendances");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.LectureCourses", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<string>("LectureId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LectureId");

                    b.ToTable("LectureCourses");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.LectureGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("LectureGroup");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.ProfessorDetail", b =>
                {
                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("BirthDay")
                        .HasColumnType("date");

                    b.Property<string>("CurrentTitleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DegreeTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Order")
                        .HasColumnType("float");

                    b.HasKey("ProfessorId");

                    b.ToTable("ProfessorDetails");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Room", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("Capacity")
                        .HasColumnType("bigint");

                    b.Property<string>("EquipmentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Semester", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("EnrollmentEndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("EnrollmentStartDate")
                        .HasColumnType("date");

                    b.Property<string>("SemesterType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.StudentCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AssistantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentStudentIndex")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssistantId");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.StudyProgram", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("StudyPrograms");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Professor", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short?>("OrderingRank")
                        .HasColumnType("smallint");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Student", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentIndex")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudyProgramCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("StudentIndex")
                        .IsUnique()
                        .HasFilter("[StudentIndex] IS NOT NULL");

                    b.HasIndex("StudyProgramCode");

                    b.ToTable("AspNetUsers", t =>
                        {
                            t.Property("Name")
                                .HasColumnName("Student_Name");
                        });

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Course", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Professor", null)
                        .WithMany("CourseAssistants")
                        .HasForeignKey("ProfessorId");

                    b.HasOne("QRCodeEvidentationApp.Models.Professor", null)
                        .WithMany("CourseProfessors")
                        .HasForeignKey("ProfessorId1");

                    b.HasOne("QRCodeEvidentationApp.Models.Semester", "Semester")
                        .WithMany("Courses")
                        .HasForeignKey("SemesterCode");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.CourseAssistant", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Assistant")
                        .WithMany()
                        .HasForeignKey("AssistantId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("QRCodeEvidentationApp.Models.Course", "Course")
                        .WithMany("CourseAssistants")
                        .HasForeignKey("CourseId");

                    b.Navigation("Assistant");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.CourseProfessor", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Course", "Course")
                        .WithMany("CourseProfessors")
                        .HasForeignKey("CourseId");

                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");

                    b.Navigation("Course");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Lecture", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");

                    b.HasOne("QRCodeEvidentationApp.Models.Room", "Room")
                        .WithMany("Lectures")
                        .HasForeignKey("RoomName");

                    b.Navigation("Professor");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.LectureAttendance", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Lecture", "Lecture")
                        .WithMany()
                        .HasForeignKey("LectureId");

                    b.HasOne("QRCodeEvidentationApp.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.LectureCourses", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("QRCodeEvidentationApp.Models.Lecture", "Lecture")
                        .WithMany("Courses")
                        .HasForeignKey("LectureId");

                    b.Navigation("Course");

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.LectureGroup", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Professor")
                        .WithMany("ProfessorLectureGroups")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.ProfessorDetail", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Professor")
                        .WithOne("ProfessorDetail")
                        .HasForeignKey("QRCodeEvidentationApp.Models.ProfessorDetail", "ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.StudentCourse", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Assistant")
                        .WithMany()
                        .HasForeignKey("AssistantId");

                    b.HasOne("QRCodeEvidentationApp.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId");

                    b.HasOne("QRCodeEvidentationApp.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");

                    b.HasOne("QRCodeEvidentationApp.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId");

                    b.Navigation("Assistant");

                    b.Navigation("Course");

                    b.Navigation("Professor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Student", b =>
                {
                    b.HasOne("QRCodeEvidentationApp.Models.StudyProgram", "StudyProgramCodeNavigation")
                        .WithMany("Students")
                        .HasForeignKey("StudyProgramCode");

                    b.Navigation("StudyProgramCodeNavigation");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Course", b =>
                {
                    b.Navigation("CourseAssistants");

                    b.Navigation("CourseProfessors");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Lecture", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Room", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Semester", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.StudyProgram", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Professor", b =>
                {
                    b.Navigation("CourseAssistants");

                    b.Navigation("CourseProfessors");

                    b.Navigation("ProfessorDetail");

                    b.Navigation("ProfessorLectureGroups");
                });

            modelBuilder.Entity("QRCodeEvidentationApp.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
