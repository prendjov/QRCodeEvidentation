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

    public StudentController(IStudentService studentService, ILectureService lectureService)
    { 
        _studentService = studentService;
        _lectureService = lectureService;
    }
    
    // Should edit cases where the student is already registered for the lecture
    // What should we do with registrations after ValidRegistrationUntil?
    [HttpGet]
    [Authorize]
    public IActionResult RegisterAttendance(string id)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;
            
        _lectureService.RegisterAttendance(student.StudentIndex, id, DateTime.Now);

        return RedirectToAction("Index", "Home");
    }
}