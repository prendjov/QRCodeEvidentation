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
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfessorService _professorService;
        private readonly ICourseService _courseService;
        private readonly ILectureService _lectureService;
        private readonly IStudentService _studentService;

        public CoursesController(ApplicationDbContext context,
            IProfessorService professorService,
            ICourseService courseService,
            ILectureService lectureService,
            IStudentService studentService)
        {
            _context = context;
            _professorService = professorService;
            _courseService = courseService;
            _lectureService = lectureService;
            _studentService = studentService;
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
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
            List<Lecture> lectures = new List<Lecture>();
            lectures = _lectureService.GetLecturesForProfessor(loggedInProfessor.Id);
            List<StudentCourse> students = new List<StudentCourse>();
            students = _studentService.GetStudentsForProfessor(loggedInProfessor.Id);

            List<string?> lecturesForCourse = _courseService.GetLectureForCourseId(id);

            CourseAnalyticsDTO courseAnalyticsDto = _courseService.GetCourseStatistics(lectures, students, lecturesForCourse);
            
            return View(courseAnalyticsDto);
        }

        // // GET: Courses/Create
        // public IActionResult Create()
        // {
        //     ViewData["SemesterCode"] = new SelectList(_context.Semesters, "Code", "Code");
        //     return View();
        // }
        //
        // // POST: Courses/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,StudyYear,LastNameRegex,SemesterCode,NumberOfFirstEnrollments,NumberOfReEnrollments,GroupPortion,Groups,English")] Course course)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(course);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["SemesterCode"] = new SelectList(_context.Semesters, "Code", "Code", course.SemesterCode);
        //     return View(course);
        // }
        //
        // // GET: Courses/Edit/5
        // public async Task<IActionResult> Edit(long? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var course = await _context.Courses.FindAsync(id);
        //     if (course == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["SemesterCode"] = new SelectList(_context.Semesters, "Code", "Code", course.SemesterCode);
        //     return View(course);
        // }
        //
        // // POST: Courses/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(long id, [Bind("Id,StudyYear,LastNameRegex,SemesterCode,NumberOfFirstEnrollments,NumberOfReEnrollments,GroupPortion,Groups,English")] Course course)
        // {
        //     if (id != course.Id)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(course);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!CourseExists(course.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["SemesterCode"] = new SelectList(_context.Semesters, "Code", "Code", course.SemesterCode);
        //     return View(course);
        // }
        //
        // // GET: Courses/Delete/5
        // public async Task<IActionResult> Delete(long? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var course = await _context.Courses
        //         .Include(c => c.Semester)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (course == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(course);
        // }
        //
        // // POST: Courses/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(long id)
        // {
        //     var course = await _context.Courses.FindAsync(id);
        //     if (course != null)
        //     {
        //         _context.Courses.Remove(course);
        //     }
        //
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
