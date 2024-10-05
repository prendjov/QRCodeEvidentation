using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ILectureService _lectureService;
    private readonly ICourseService _courseService;

    public StudentController(IStudentService studentService, ILectureService lectureService, ICourseService courseService)
    { 
        _studentService = studentService;
        _lectureService = lectureService;
        _courseService = courseService;
    }
    
    // Should edit cases where the student is already registered for the lecture
    // What should we do with registrations after ValidRegistrationUntil?
    [HttpGet]
    [Authorize]
    public IActionResult RegisterAttendance(string id)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;

        long? courseId = _courseService.GetCourseIdByLectureId(id);

        bool inCourse = _studentService.CheckStudentInCourse(student.StudentIndex, courseId);

        if (inCourse)
        {
            _lectureService.RegisterAttendance(student, id, DateTime.Now);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}