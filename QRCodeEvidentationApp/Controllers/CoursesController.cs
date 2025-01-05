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
        private readonly IProfessorService _professorService;
        private readonly ICourseService _courseService;

        public CoursesController(
            IProfessorService professorService,
            ICourseService courseService)
        {
            _professorService = professorService;
            _courseService = courseService;
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
        public async Task<IActionResult> Details(long id)
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

            var course = _courseService.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        
        public IActionResult DisplayError(string error)
        {
            ErrorMessageDTO errorMessageDto = new ErrorMessageDTO();
            errorMessageDto.Message = error;
            return View(errorMessageDto);
        }
    }
}
