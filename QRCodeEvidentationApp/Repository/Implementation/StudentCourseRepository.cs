using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class StudentCourseRepository : IStudentCourseRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<StudentCourse> _entities;

    public StudentCourseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<StudentCourse>();
    }
    
    public bool CheckStudentCourse(string studentId, long? courseId)
    {
        var isEnrolled = _entities
            .Any(sc => sc.StudentStudentIndex == studentId && sc.CourseId == courseId);

        return isEnrolled;
    }
}