using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.Parsers;
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
        return _entities.Where(d => d.Id == lectureId).Include(d => d.LectureGroup).ThenInclude(dc => dc.Courses).FirstOrDefault();
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

    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId)
    {
        return _entities
            .Where(l => l.ProfessorId == professorId && l.LectureGroupId == courseGroupId)
            .ToList();    }

    public void BulkInsertLectures(List<LectureCsvParser> lectureCsvFormat, string professorEmail)
    {
        Professor professor = _context.Professors.Where(p => p.Email == professorEmail).FirstOrDefault();
        foreach (var record in lectureCsvFormat)
        {
            Lecture lecture = new Lecture
            {
                Id = Guid.NewGuid().ToString(),
                Title = record.Title ?? string.Empty,
                StartsAt = record.StartsAt,
                EndsAt = record.EndsAt,
                ProfessorId = professor.Id,
                Professor = professor,
                Type = record.Type,
                ValidRegistrationUntil = record.ValidRegistrationUntil,
                LectureGroupId = record.GroupCourseId
            };
            
            _entities.Add(lecture);
        }
        
        _context.SaveChangesAsync();
    }
}