using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Student> _entities;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<Student>();
    }
    
    public async Task<Student> GetStudentByEmail(string email)
    {
        return await _entities.SingleOrDefaultAsync(x => x.Email.Equals(email)) ?? throw new InvalidOperationException();
    }

    public List<long?> GetCoursesForStudent(string index)
    {
        Student student = _entities.Where(i => i.StudentIndex.Equals(index)).Include("StudentCourses").FirstOrDefault();
        List<long?> courses = new List<long?>();
        
        foreach (var obj in student.StudentCourses)
        {
            courses.Add(obj.CourseId);
        }

        return courses;
    }
}