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
    public class LectureGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourseService _courseService;
        private readonly ILectureGroupService _lectureGroupService;
        private readonly IProfessorService _professorService;
        private readonly ILectureService _lectureService;
        private readonly IStudentService _studentService;

        public LectureGroupsController(ApplicationDbContext context, 
            ICourseService courseService, 
            ILectureGroupService lectureGroupService, 
            IProfessorService professorService,
            ILectureService lectureService,
            IStudentService studentService)
        {
            _context = context;
            _courseService = courseService;
            _lectureGroupService = lectureGroupService;
            _professorService = professorService;
            _lectureService = lectureService;
            _studentService = studentService;
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

        // GET: LectureGroups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureGroup = await _context.LectureGroup.FindAsync(id);
            if (lectureGroup == null)
            {
                return NotFound();
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Id", lectureGroup.ProfessorId);
            return View(lectureGroup);
        }

        // POST: LectureGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ProfessorId")] LectureGroup lectureGroup)
        {
            if (id != lectureGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lectureGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureGroupExists(lectureGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "Id", lectureGroup.ProfessorId);
            return View(lectureGroup);
        }

        // GET: LectureGroups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
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
            
            return View(lectureGroupAnalyticsDto);
        }
    }
}
