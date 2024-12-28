using QRCodeEvidentationApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Lecture> Lectures { get; set; }

        public virtual DbSet<LectureAttendance> LectureAttendances { get; set; }

        public virtual DbSet<LectureCourses> LectureCourses { get; set; }

        public virtual DbSet<Professor> Professors { get; set; }

        public virtual DbSet<ProfessorDetail> ProfessorDetails { get; set; }

        // public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        public virtual DbSet<StudyProgram> StudyPrograms { get; set; }

        public virtual DbSet<CourseAssistant> CourseAssistants { get; set; }

        public virtual DbSet<CourseProfessor> CourseProfessors{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LectureAttendance>(entity =>
            {
                entity.HasOne(e => e.Lecture)
                    .WithMany()
                    .HasForeignKey(e => e.LectureId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<CourseAssistant>(entity =>
            {
                // Configure the relationship with Professor (Assistant)
                entity.HasOne(e => e.Assistant)
                      .WithMany()
                      .HasForeignKey(e => e.AssistantId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<QRCodeEvidentationApp.Models.LectureGroup> LectureGroup { get; set; } = default!;
    }
}
