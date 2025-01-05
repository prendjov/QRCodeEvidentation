using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ILectureService _lectureService;
    private readonly ILectureAttendanceService _lectureAttendanceService;

    public StudentController(IStudentService studentService,
        ILectureService lectureService,
        ICourseService courseService,
        ILectureAttendanceService lectureAttendanceService)
    { 
        _studentService = studentService;
        _lectureService = lectureService;
        _lectureAttendanceService = lectureAttendanceService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult RegisterAttendance(string id)
    {
        ErrorMessageDTO message = new ErrorMessageDTO();
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;
        
        LectureAttendance? attendance = _lectureAttendanceService.FindStudentRegistration(student.StudentIndex, id);

        if (attendance != null)
        {
            message.Message += "\nYou are already registered for this lecture.";
            return View(message);
        }
        
        _lectureService.RegisterAttendance(student, id, DateTime.Now);
        return RedirectToAction("Index", "Home");
    }
}