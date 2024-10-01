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

    public long? GetCourseForLecture(string? lectureId)
    {
        LectureCourses temp = _entities.FirstOrDefault(c => c.LectureId == lectureId) ?? throw new InvalidOperationException();
        return temp.CourseId;
    }
}