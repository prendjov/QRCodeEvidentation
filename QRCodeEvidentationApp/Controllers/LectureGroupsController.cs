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

namespace QRCodeEvidentationApp.Controllers
{
    [Authorize(Roles = "PROFESSOR")]
    public class LectureGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourseService _courseService;
        private readonly ILectureGroupService _lectureGroupService;
        private readonly IProfessorService _professorService;
        private readonly ILectureService _lectureService;
        private readonly IStudentService _studentService;
        private readonly ILectureAttendanceService _lectureAttendanceService;
        private readonly IGeneratePDFDocument _generatePdfDocument;

        public LectureGroupsController(ApplicationDbContext context, 
            ICourseService courseService, 
            ILectureGroupService lectureGroupService, 
            IProfessorService professorService,
            ILectureService lectureService,
            IStudentService studentService,
            ILectureAttendanceService lectureAttendanceService,
            IGeneratePDFDocument generatePdfDocument)
        {
            _context = context;
            _courseService = courseService;
            _lectureGroupService = lectureGroupService;
            _professorService = professorService;
            _lectureService = lectureService;
            _studentService = studentService;
            _lectureAttendanceService = lectureAttendanceService;
            _generatePdfDocument = generatePdfDocument;
        }

        private async Task<bool> IsUserCreatorOfLectureGroup(string lectureGroupId)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var professor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
    
            var lectureGroup = await _lectureGroupService.Get(lectureGroupId);
            return lectureGroup?.ProfessorId == professor.Id;
        }
        
        // GET: LectureGroups
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor professor = _professorService.GetProfessorFromUserEmail(userEmail).Result;

            List<LectureGroup> lectureGroups = _lectureGroupService.ListByProfessor(professor.Id).Result;
            return View(lectureGroups);
        }

        // GET: LectureGroups/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }
            
            LectureGroup lecture = await _lectureGroupService.Get(id);

            return View(lecture);
        }

        // GET: LectureGroups/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor professor = _professorService.GetProfessorFromUserEmail(userEmail).Result;
            
            LectureGroupDTO data = await _lectureGroupService.PrepareForCreate(professor.Id);

            return View(data);
        }

        // POST: LectureGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,SelectedCourses,ProfessorId,Courses")] LectureGroupDTO data)
        {            
            if (ModelState.IsValid)
            {
                _lectureGroupService.Create(data);

                return RedirectToAction(nameof(Index));
            }
            data = await _lectureGroupService.PrepareForCreate(User.Identity.Name);

            return View(data);
        }
        
        // GET: LectureGroups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }

            var lectureGroup = await _context.LectureGroup
                .Include(l => l.Professor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lectureGroup == null)
            {
                return NotFound();
            }

            return View(lectureGroup);
        }

        // POST: LectureGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lectureGroup = await _context.LectureGroup.FindAsync(id);
            if (lectureGroup != null)
            {
                _context.LectureGroup.Remove(lectureGroup);
            }
            
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectureGroupExists(string id)
        {
            return _context.LectureGroup.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalytics(string id)
        {
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }
            
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
            List<Lecture> lectures = new List<Lecture>();
            lectures = _lectureService.GetLecturesForProfessor(loggedInProfessor.Id);
            List<StudentCourse> students = new List<StudentCourse>();
            students = _studentService.GetStudentsForProfessor(loggedInProfessor.Id);
            
            List<long?> lectureGroupCourses = _lectureGroupService.GetCoursesForLectureGroup(id).Result;
            
            List<string> lecturesByLectureGroup = _lectureGroupService.SelectLecturesForGroup(lectures, lectureGroupCourses);

            LectureGroupAnalyticsDTO lectureGroupAnalyticsDto =
                _lectureGroupService.CalculateLectureGroupAnalytics(lecturesByLectureGroup, students, lectures);

            lectureGroupAnalyticsDto.groupId = id;
            return View(lectureGroupAnalyticsDto);
        }
        
        public async Task<IActionResult> GeneralAnalytics(string id)
        {
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }
            
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
            List<Lecture> lectures = new List<Lecture>();
            lectures = _lectureService.GetLecturesForProfessor(loggedInProfessor.Id);
            
            List<long?> lectureGroupCourses = _lectureGroupService.GetCoursesForLectureGroup(id).Result;
            
            List<string> lecturesForCourse = _lectureGroupService.SelectLecturesForGroup(lectures, lectureGroupCourses);
            
            // take all the lectures that have id in the list of string 
            List<Lecture> lectureObjects = _lectureGroupService.GetLectures(lecturesForCourse);
            
            List<Student> studentsForLectureGroup = new List<Student>();
            foreach (long? courseId in lectureGroupCourses)
            {
                List<Student> students = _studentService.GetStudentsForCourse(courseId);
                studentsForLectureGroup.AddRange(students);
            }
            
            List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalytics = new List<AggregatedCourseAnalyticsDto>();
            
            foreach (Student student in studentsForLectureGroup)
            {
                AggregatedCourseAnalyticsDto aggregatedCourseAnalyticsDto = new AggregatedCourseAnalyticsDto();
                aggregatedCourseAnalyticsDto.Student = student;
                aggregatedCourseAnalyticsDto.LectureAttendance = new List<LectureAttendanceAnalyticDto>();
                List<LectureAttendance> lectureAttendancesForStudent =
                    _lectureAttendanceService.GetLectureAttendanceForStudent(student).Result;

                foreach (Lecture lecture in lectureObjects)
                {
                    LectureAttendanceAnalyticDto lectureAttendanceAnalyticDto = new LectureAttendanceAnalyticDto();
                    lectureAttendanceAnalyticDto.Lecture = lecture;
                    bool IsAttended = lectureAttendancesForStudent
                        .Any(attendance => attendance.LectureId == lecture.Id);

                    if (IsAttended)
                    {
                        lectureAttendanceAnalyticDto.IsPresent = 1;
                    }
                    else
                    {
                        lectureAttendanceAnalyticDto.IsPresent = 0;
                    }

                    aggregatedCourseAnalyticsDto.LectureAttendance.Add(lectureAttendanceAnalyticDto);
                }
                
                aggregatedCourseAnalytics.Add(aggregatedCourseAnalyticsDto);
            }
            
            return _generatePdfDocument.GenerateDocument(aggregatedCourseAnalytics);
        }
        
        public IActionResult DisplayError(string error)
        {
            ErrorMessageDTO errorMessageDto = new ErrorMessageDTO();
            errorMessageDto.Message = error;
            return View(errorMessageDto);
        }
    }
}
