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
        private readonly ILectureGroupService _lectureGroupService;
        private readonly IProfessorService _professorService;
        private readonly ILectureService _lectureService;
        private readonly IGenerateExcelDocument _generateDocumentService;
        private readonly ICourseService _courseService;
        
        public LectureGroupsController( 
            ILectureGroupService lectureGroupService, 
            IProfessorService professorService,
            ILectureService lectureService,
            IGenerateExcelDocument generateDocumentService)
        {
            _lectureGroupService = lectureGroupService;
            _professorService = professorService;
            _lectureService = lectureService;
            _generateDocumentService = generateDocumentService;
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
        public IActionResult Create([Bind("Name,SelectedCourses,ProfessorId,Courses")] LectureGroupDTO data)
        {            
            if (ModelState.IsValid)
            {
                _lectureGroupService.Create(data);

                return RedirectToAction(nameof(Index));
            }
            data = _lectureGroupService.PrepareForCreate(User.Identity.Name).Result;

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

            var lectureGroup = _lectureGroupService.Get(id).Result;
            
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
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }

            _lectureGroupService.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult DisplayError(string error)
        {
            ErrorMessageDTO errorMessageDto = new ErrorMessageDTO();
            errorMessageDto.Message = error;
            return View(errorMessageDto);
        }
        
         public async Task<IActionResult> Analytics(string id)
        {
            if (!await IsUserCreatorOfLectureGroup(id))
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture group." });
            }
            
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
            
            List<Lecture> lectures = new List<Lecture>();
            lectures = _lectureService.GetLecturesByProfessorAndCourseGroupId(loggedInProfessor.Id, id);
            CourseGroupAnalyticsDTO courseAnalytics = _lectureGroupService.GetLecturesCourseGroupAnalytics(lectures, id);
            return View(courseAnalytics);
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
            
            var result = _generateDocumentService.GenerateDocument(loggedInProfessor, id);

            LectureGroup lectureGroup = _lectureGroupService.Get(id).Result;

            if (result is FileContentResult fileResult)
            {
                // Change the FileDownloadName
                fileResult.FileDownloadName = "aggregated_analytics_" + lectureGroup.Name + "_" + DateTime.Now + ".xlsx";
                return fileResult;
            }
            
            return result;
        }
    }
}
