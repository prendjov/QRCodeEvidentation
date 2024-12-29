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
        private readonly ILectureGroupService _lectureGroupService;
        private readonly IProfessorService _professorService;


        public LectureGroupsController(ApplicationDbContext context, 
            ILectureGroupService lectureGroupService, 
            IProfessorService professorService)
        {
            _context = context;
            _lectureGroupService = lectureGroupService;
            _professorService = professorService;
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
        
        public IActionResult DisplayError(string error)
        {
            ErrorMessageDTO errorMessageDto = new ErrorMessageDTO();
            errorMessageDto.Message = error;
            return View(errorMessageDto);
        }
    }
}
