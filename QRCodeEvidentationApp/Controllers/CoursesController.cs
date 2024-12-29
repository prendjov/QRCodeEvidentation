using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Service.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QRCodeEvidentationApp.Controllers
{
    [Authorize(Roles = "PROFESSOR")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfessorService _professorService;
        private readonly ICourseService _courseService;
        private readonly ILectureService _lectureService;
        private readonly IStudentService _studentService;
        private readonly ILectureAttendanceService _lectureAttendanceService;
        private readonly IGenerateExcelDocument _generatePdfDocument;

        public CoursesController(ApplicationDbContext context,
            IProfessorService professorService,
            ICourseService courseService,
            ILectureService lectureService,
            IStudentService studentService,
            ILectureAttendanceService lectureAttendanceService,
            IGenerateExcelDocument generatePdfDocument)
        {
            _context = context;
            _professorService = professorService;
            _courseService = courseService;
            _lectureService = lectureService;
            _studentService = studentService;
            _lectureAttendanceService = lectureAttendanceService;
            _generatePdfDocument = generatePdfDocument;
        }
        
        private bool IsProfessorAssignedCourse(long? courseId)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor professor = _professorService.GetProfessorFromUserEmail(userEmail).Result;

            bool result = _courseService.ProfessorAtCourse(courseId, professor.Id);
            return result;
        }

        // GET: Courses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor professor = _professorService.GetProfessorFromUserEmail(userEmail).Result;

            List<Course> courses = _courseService.GetCourses(professor.Id);
            
            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!IsProfessorAssignedCourse(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "You are not assigned as professor to this course." });
            }

            var course = await _context.Courses
                .Include(c => c.Semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        
        public async Task<IActionResult> Analytics(long? id)
        {
            if (!IsProfessorAssignedCourse(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "You are not assigned as professor to this course." });
            }
            
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
            
            // TODO: IF NUMBEROFSTUDENTSATPROFFESOR / NOTATPROFESSOR IS NOT NEEDED, REMOVE SOME OF THE LOGIC HERE.
            List<Lecture> lectures = new List<Lecture>();
            lectures = _lectureService.GetLecturesByProfessorAndCourseId(loggedInProfessor.Id, id);
            CourseAnalyticsDTO courseAnalytics = new CourseAnalyticsDTO();
            courseAnalytics.lecturesAndAttendees = new Dictionary<Lecture, long>();
            courseAnalytics.courseId = id;
            foreach (Lecture l in lectures)
            {
                List<LectureAttendance> lectureAttendances = _lectureAttendanceService.GetLectureAttendance(l.Id).Result;
                courseAnalytics.lecturesAndAttendees[l] = lectureAttendances.Count;
            }
            
            // List<StudentCourse> students = new List<StudentCourse>();
            // students = _studentService.GetStudentsForProfessor(loggedInProfessor.Id);
            //
            // List<string?> lecturesForCourse = _courseService.GetLectureForCourseId(id);
            //
            // CourseAnalyticsDTO courseAnalyticsDto = _courseService.GetCourseStatistics(lectures, students, lecturesForCourse);

            // courseAnalyticsDto.courseId = id;
            
            return View(courseAnalytics);
        }

        public async Task<IActionResult> GeneralAnalytics(long? id)
        {
            if (!IsProfessorAssignedCourse(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "You are not assigned as professor to this course." });
            }
            
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());


            List<Lecture> lectures = _lectureService.GetLecturesByProfessorAndCourseId(loggedInProfessor.Id, id);
            HashSet<Student> students = new HashSet<Student>();

            foreach (Lecture l in lectures)
            {
                List<LectureAttendance> lectureAttendances = _lectureAttendanceService.GetLectureAttendance(l.Id).Result;
                foreach (LectureAttendance attendance in lectureAttendances)
                {
                    students.Add(attendance.Student);
                }
            }

            List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalyticsDtos = new List<AggregatedCourseAnalyticsDto>();
            foreach (Student s in students)
            {
                AggregatedCourseAnalyticsDto singleAnalytic = new AggregatedCourseAnalyticsDto();
                singleAnalytic.Student = s;
                singleAnalytic.LectureAndAttendance = new Dictionary<string, long>();
                singleAnalytic.totalAttendances = 0;

                foreach (Lecture l in lectures)
                {
                    singleAnalytic.LectureAndAttendance[l.Id] = 0;
                }
                
                List<LectureAttendance> attendances = _lectureAttendanceService.GetLectureAttendanceForStudent(s).Result;

                foreach (LectureAttendance attendance in attendances)
                {
                    if (singleAnalytic.LectureAndAttendance.Keys.Contains(attendance.LectureId))
                    {
                        singleAnalytic.LectureAndAttendance[attendance.LectureId] = 1;
                        singleAnalytic.totalAttendances += 1;
                    }
                }
                
                aggregatedCourseAnalyticsDtos.Add(singleAnalytic);
            }

            return _generatePdfDocument.GenerateDocument(aggregatedCourseAnalyticsDtos, lectures);
        }
        
        public IActionResult DisplayError(string error)
        {
            ErrorMessageDTO errorMessageDto = new ErrorMessageDTO();
            errorMessageDto.Message = error;
            return View(errorMessageDto);
        }
    }
}
