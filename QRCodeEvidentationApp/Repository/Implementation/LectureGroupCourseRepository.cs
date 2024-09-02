using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation
{
    public class LectureGroupCourseRepository : ILectureGroupCourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<LectureGroupCourse> _entities;

        public LectureGroupCourseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<LectureGroupCourse>();
        }

        public async Task<LectureGroupCourse> Create(LectureGroupCourse lectureGroupCourse)
        {
            var createdLectureGroupCourse = _entities.Add(lectureGroupCourse);
            _context.SaveChanges();

            return createdLectureGroupCourse.Entity;
        }

        public async Task<LectureGroupCourse> Delete(LectureGroupCourse lectureGroupCourse)
        {
            _entities.Remove(lectureGroupCourse);
            await _context.SaveChangesAsync();

            return lectureGroupCourse;

        }

        public async Task<List<LectureGroupCourse>> ListByLectureGroupId(string lectureGroupId)
        {
            return await _entities.Where(l => l.LectureGroupId == lectureGroupId).ToListAsync();
        }
    }
}
