using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class LectureRepository : ILectureRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Lecture> _entities;
    
    public LectureRepository(ApplicationDbContext context)
    {
        this._context = context;
        _entities = context.Set<Lecture>();
    }
    
    public async Task<List<Lecture>> GetAllByProfessor(string? professorId)
    {
        return await _entities.Where(l => l.ProfessorId != null && l.ProfessorId.Equals(professorId)).ToListAsync();
    }

    public async Task<Lecture> GetLectureByProfessorId(string? professorId)
    {
        return await _entities.FindAsync(professorId) ?? throw new InvalidOperationException();
    }

    public async Task<Lecture> GetLectureById(string? lectureId)
    {
        return await _entities.FindAsync(lectureId) ?? throw new InvalidOperationException();
    }

    public async Task<List<Lecture>> FilterLectureByDateOrCourse(DateOnly? dateFrom, DateOnly? dateTo, List<long>? coursesIds)
    {
        var query = _entities.AsQueryable();

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

    public Lecture UpdateLecture(Lecture lecture)
    {
        ArgumentNullException.ThrowIfNull(lecture);
        _entities.Update(lecture);
        _context.SaveChanges();
        return lecture;
    }

    public async Task<Lecture> DeleteLecture(Lecture lecture)
    {
        _entities.Remove(lecture);
        await _context.SaveChangesAsync();

        return lecture;
    }

    public async Task<Lecture> CreateNewLecture(Lecture lecture)
    {
        var createdLecture = await _entities.AddAsync(lecture);
        await _context.SaveChangesAsync();

        return createdLecture.Entity;
    }
}