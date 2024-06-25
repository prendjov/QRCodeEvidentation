using FinkiEvidentationProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QRCodeEvidentationApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthUser> AuthUsers { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<CourseProfessorEvaluation> CourseProfessorEvaluations { get; set; }

        public virtual DbSet<CourseStudentGroup> CourseStudentGroups { get; set; }

        public virtual DbSet<JoinedSubject> JoinedSubjects { get; set; }

        public virtual DbSet<Lecture> Lectures { get; set; }

        public virtual DbSet<LectureAttendance> LectureAttendances { get; set; }

        public virtual DbSet<LectureCourses> LectureCourses { get; set; }

        public virtual DbSet<Professor> Professors { get; set; }

        public virtual DbSet<ProfessorDetail> ProfessorDetails { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        public virtual DbSet<StudentGroup> StudentGroups { get; set; }

        public virtual DbSet<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; }

        public virtual DbSet<StudyProgram> StudyPrograms { get; set; }

        public virtual DbSet<StudyProgramSubject> StudyProgramSubjects { get; set; }

        public virtual DbSet<StudyProgramSubjectProfessor> StudyProgramSubjectProfessors { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<SubjectDetail> SubjectDetails { get; set; }

        public virtual DbSet<SubjectNameMapping> SubjectNameMappings { get; set; }

        public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Assistant)
                .WithMany(p => p.CourseAssistants)
                .HasForeignKey(c => c.AssistantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(p => p.CourseProfessors)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
