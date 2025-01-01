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
        _context = context;
        _entities = context.Set<Lecture>();
    }
    
    public async Task<List<Lecture>> GetAllByProfessor(string? professorId)
    {
        return await _entities.Where(l => l.ProfessorId != null && l.ProfessorId.Equals(professorId))
            .Include("Courses")
            .ToListAsync();
    }

    public async Task<Lecture> GetLectureByProfessorId(string? professorId)
    {
        return await _entities.Include("Professor").Where(z => z.ProfessorId != null && z.ProfessorId.Equals(professorId)).FirstOrDefaultAsync();
    }

    public async Task<Lecture> GetLectureById(string? lectureId)
    {
        return _entities.Where(d => d.Id == lectureId).Include(l => l.LectureGroup).Include(d => d.Courses).ThenInclude(dc => dc.Course).FirstOrDefault();
    }

    public async Task<List<Lecture>> FilterLectureByDateOrCourse(DateTime? dateFrom, DateTime? dateTo, List<long>? coursesIds)
    {
        // var filterByDate = _entities.Where(l =>
        //     l.StartsAt >= dateFrom && l.EndsAt <= dateTo);
        //
        // if (coursesIds != null)
        // {
        //     filterByDate
        // }
        var query = _entities.AsQueryable();

        if (dateFrom.HasValue)
        {
            query = query.Where(l => l.StartsAt >= dateFrom);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(l => l.StartsAt <= dateTo);
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

    public List<Lecture> GetLecturesByIds(List<string> lectureIds)
    {
        return _entities.Where(l => lectureIds.Contains(l.Id)).ToList();
    }

    public List<Lecture> GetLecturesForProfessorFiltered(
        string? professorId, 
        int page, 
        int pageSize, 
        int startsAtSorting, 
        string lectureTypeFilter, 
        out int totalLectures)
    {
        var skip = (page - 1) * pageSize;

        // Base query with professor filter
        var query = _context.Lectures
            .Where(l => l.ProfessorId == professorId);

        // Apply lecture type filter if specified
        if (lectureTypeFilter == "Предавања" || lectureTypeFilter == "Аудиториски")
        {
            query = query.Where(l => l.Type == lectureTypeFilter);
        }

        // Apply sorting based on startsAtSorting
        query = startsAtSorting == 0 
            ? query.OrderByDescending(l => l.StartsAt) 
            : query.OrderBy(l => l.StartsAt);

        // Get total lectures for pagination
        totalLectures = query.Count();

        // Paginate results
        var lectures = query
            .Skip(skip)
            .Take(pageSize)
            .ToList();

        return lectures;
    }
    
    public List<Lecture> GetLecturesByProfessorAndCourseId(string? professorId, long? courseId)
    {
        return _entities
            .Where(l => l.ProfessorId == professorId && 
                        l.Courses.Any(c => c.CourseId == courseId))
            .ToList();
    }

    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId)
    {
        return _entities
            .Where(l => l.ProfessorId == professorId && l.LectureGroupId == courseGroupId)
            .ToList();    }
}