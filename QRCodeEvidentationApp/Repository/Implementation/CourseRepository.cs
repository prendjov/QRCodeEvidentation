using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QRCodeEvidentationApp.Models.DTO;

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

        public async Task<List<string?>> GetLectureForCourseId(long? id)
        {
            return await _context.LectureCourses.Where(l => l.CourseId.Equals(id)).Select(l => l.LectureId).ToListAsync();
        }
    }
}
