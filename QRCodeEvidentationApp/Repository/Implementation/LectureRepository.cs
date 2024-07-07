using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class LectureRepository : ILectureRepository
{
    private readonly ApplicationDbContext _context;
    private DbSet<Lecture> entities;
    
    public LectureRepository(ApplicationDbContext context)
    {
        this._context = context;
        entities = context.Set<Lecture>();
    }
    
    public async Task<List<Lecture>> GetAllByProfessor(string? professorId)
    {
        return await entities.Where(l => l.ProfessorId != null && l.ProfessorId.Equals(professorId)).ToListAsync();
    }

    public async Task<Lecture> GetLectureByProfessorId(string? professorId)
    {
        return await entities.FindAsync(professorId) ?? throw new InvalidOperationException();
    }

    public async Task<Lecture> GetLectureById(string? lectureId)
    {
        return await entities.FindAsync(lectureId) ?? throw new InvalidOperationException();
    }

    public async Task<List<Lecture>> FilterLectureByDateOrCourse(DateOnly? dateFrom, DateOnly? dateTo, List<long>? coursesIds)
    {
        var query = entities.AsQueryable();

        if (dateFrom.HasValue)
        {
            query = query.Where(l => l.StartsAt.Date >= dateFrom.Value.ToDateTime(TimeOnly.MinValue));
        }

        if (dateTo.HasValue)
        {
            query = query.Where(l => l.StartsAt.Date <= dateTo.Value.ToDateTime(TimeOnly.MaxValue));
        }

        if (coursesIds != null)
        {
            query = query.Where(l => l.Courses.Any(c => c.CourseId != null && coursesIds.Contains(c.CourseId.Value)));
        }
        
        return await query.ToListAsync();
    }

    public Task<Lecture> EditLecture(string? lectureId)
    {
        throw new NotImplementedException();
    }

    public Task<Lecture> DisableLecture(string? lectureId)
    {
        throw new NotImplementedException();
    }
}