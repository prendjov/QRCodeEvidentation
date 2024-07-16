using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class CourseRepository<T> : ICourseRepository<T> where T : CourseUserBaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;
    
    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    
    public async Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId)
    {
        return await _context.Set<CourseProfessor>()
            .Where(cp => cp.Id == professorId)
            .ToListAsync();
    }

    public async Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId)
    {
        return await _context.Set<CourseAssistant>()
            .Where(cp => cp.Id == assistantId)
            .ToListAsync();    
    }
}