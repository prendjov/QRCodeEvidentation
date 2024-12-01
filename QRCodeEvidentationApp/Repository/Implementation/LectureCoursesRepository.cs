using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class LectureCoursesRepository : ILectureCoursesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<LectureCourses> _entities;
    
    public LectureCoursesRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<LectureCourses>();
    }
    
    public LectureCourses CreateLectureCourse(LectureCourses lectureCourse)
    {
        var createdLectureCourse = _entities.Add(lectureCourse); 
        _context.SaveChanges();

        return createdLectureCourse.Entity;
    }
    public LectureCourses DeleteLectureCourse(LectureCourses lectureCourse)
    {
        var createdLectureCourse = _entities.Remove(lectureCourse);
        _context.SaveChanges();

        return createdLectureCourse.Entity;

    }

    public List<long?> GetCoursesForLecture(string? lectureId)
    {
        List<LectureCourses> temp = _entities.Where(c => c.LectureId == lectureId).ToList();
        List<long?> ids = new List<long?>();

        foreach (var lc in temp)
        {
            ids.Add(lc.CourseId);
        }
        
        return ids;
    }

    public List<LectureCourses> GetUpcomingLecturesForStudent(List<StudentCourse> studentCourses)
    {
        // Flatten the list of course and professor IDs from the student's courses
        var courseProfessorPairs = studentCourses
            .Where(sc => sc.CourseId.HasValue && !string.IsNullOrEmpty(sc.ProfessorId))
            .Select(sc => new { CourseId = sc.CourseId.Value, ProfessorId = sc.ProfessorId })
            .ToList();

        // Query the LectureCourses table
        var upcomingLectures = _entities
            .Where(lc => lc.CourseId.HasValue && lc.Lecture != null
                                              && courseProfessorPairs.Any(cp => 
                                                  cp.CourseId == lc.CourseId &&
                                                  cp.ProfessorId == lc.Lecture.ProfessorId)
                                              && lc.Lecture.StartsAt > DateTime.Now)
            .Include(x => x.Lecture) // Only future lectures
            .ToList();

        return upcomingLectures;
    }
}