using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

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

        public async Task<List<Course>> GetCourses(string? teacherId)
        { 
            List<CourseProfessor> courseProfessorList = _context.CourseProfessors
                .Where(x => x.ProfessorId.Equals(teacherId)).ToList();

            List<CourseAssistant> courseAssistantsList = _context.CourseAssistants
                .Where(x => x.AssistantId.Equals(teacherId)).ToList();

            // Initialize a HashSet to store unique CourseIds
            HashSet<long?> uniqueCourseIds = new HashSet<long?>();

            // Add CourseIds from courseProfessorList
            foreach (var courseProfessor in courseProfessorList)
            {
                uniqueCourseIds.Add(courseProfessor.CourseId);
            }

            // Add CourseIds from courseAssistantsList
            foreach (var courseAssistant in courseAssistantsList)
            {
                uniqueCourseIds.Add(courseAssistant.CourseId);
            }
            
            // Use the unique CourseIds to retrieve the corresponding Courses
            List<Course> courses = await _context.Courses
                .Where(course => uniqueCourseIds.Contains(course.Id))
                .ToListAsync();

            return courses;
        }
        
        public async Task<CourseProfessor?> GetCourseProfessorCombo(long? id, string? teacherId)
        {
            return await _context.CourseProfessors.Where(x => x.ProfessorId == teacherId && x.CourseId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<CourseAssistant?> GetCourseAssistantCombo(long? id, string? teacherId)
        {
            return await _context.CourseAssistants.Where(x => x.AssistantId == teacherId && x.CourseId == id)
                .FirstOrDefaultAsync();
        }

        public Course Get(long courseId)
        {
            return _context.Courses.Where(c => c.Id == courseId).Include(c => c.Semester).FirstOrDefault();
        }
    }
}
