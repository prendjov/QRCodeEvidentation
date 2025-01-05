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
        
        public virtual DbSet<Professor> Professors { get; set; }
        
        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        
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
            
            modelBuilder.Entity<Course>()
                .HasMany(c => c.LectureGroups)
                .WithMany(lg => lg.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseLectureGroup", // Junction table name
                    j => j.HasOne<LectureGroup>().WithMany().HasForeignKey("LectureGroupId"),
                    j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                    j =>
                    {
                        j.HasKey("LectureGroupId", "CourseId"); // Composite key
                        j.ToTable("CustomCourseLectureGroupTable"); // Table name
                    });
            
            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.LectureGroup)
                .WithMany(lg => lg.Lectures)
                .HasForeignKey(l => l.LectureGroupId)
                .OnDelete(DeleteBehavior.Cascade); 
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<QRCodeEvidentationApp.Models.LectureGroup> LectureGroup { get; set; } = default!;
    }
}
