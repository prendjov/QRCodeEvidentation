using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation
{
    public class LectureGroupRepository : ILectureGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<LectureGroup> _entities;

        public LectureGroupRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<LectureGroup>();
        }

        public async Task<LectureGroup> Create(LectureGroup lectureGroup)
        {
            var createdLectureGroup = _entities.Add(lectureGroup);
            _context.SaveChanges();

            return createdLectureGroup.Entity;

        }

        public async Task<LectureGroup> Delete(LectureGroup lectureGroup)
        {
            _entities.Remove(lectureGroup);
            await _context.SaveChangesAsync();

            return lectureGroup;
        }

        public async Task<LectureGroup> GetById(string lectureGroupId)
        {
            return await _entities.Where(d => d.Id == lectureGroupId)
                .Include(gc => gc.Courses)
                .ThenInclude(course => course.Course)
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException();
        }

        public async Task<List<LectureGroup>> ListByProfessorId(string professorId)
        {
            return await _entities
                .Where(p => p.ProfessorId == professorId)
                .Include(gc => gc.Courses)
                .ToListAsync();
        }

        public async Task<LectureGroup> Update(LectureGroup lectureGroup)
        {
            ArgumentNullException.ThrowIfNull(lectureGroup);

            _entities.Update(lectureGroup);
            _context.SaveChanges();

            return lectureGroup;
        }
    }
}
