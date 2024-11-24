using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class LectureAttendanceRepository : ILectureAttendanceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<LectureAttendance> _entities;
    
    public LectureAttendanceRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<LectureAttendance>();
    }
    
    
    public void RegisterAttendance(LectureAttendance lectureAttendance)
    {
        _entities.Add(lectureAttendance);
        _context.SaveChanges();
    }

    public Task<List<LectureAttendance>> GetLectureAttendance(string? lectureId)
    {
        return _entities.Where(l => l.LectureId == lectureId)
            .Include(l => l.Student)
            .ToListAsync();
    }

    public List<LectureAttendance> GetLectureAttendances(List<string> lectureIds)
    {
        List<LectureAttendance> matchedLectureIds = _entities
            .Where(l => lectureIds.Contains(l.LectureId))
            .ToList();

        return matchedLectureIds;
    }

    public async Task<List<LectureAttendance>> GetLectureAttendancesByStudent(string? studentIndex)
    {
        return await _entities.Where(l => l.StudentIndex == studentIndex).ToListAsync();
    }
}