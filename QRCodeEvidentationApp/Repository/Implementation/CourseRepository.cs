using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRCodeEvidentationApp.Repository.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId)
        {
            if (string.IsNullOrEmpty(professorId))
            {
                return new List<CourseProfessor>();
            }

            return await _context.CourseProfessors
                .Where(cp => cp.ProfessorId == professorId)
                .Include(cp => cp.Course)
                .ToListAsync();
        }

        public async Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId)
        {
            if (string.IsNullOrEmpty(assistantId))
            {
                return new List<CourseAssistant>();
            }

            return await _context.CourseAssistants
                .Where(ca => ca.AssistantId == assistantId)
                .Include(ca => ca.Course)
                .ToListAsync();
        }
    }
}
