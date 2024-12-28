using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO.StudentDTO;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ILectureService _lectureService;
    private readonly ICourseService _courseService;
    private readonly ILectureAttendanceService _lectureAttendanceService;

    public StudentController(IStudentService studentService,
        ILectureService lectureService,
        ICourseService courseService,
        ILectureAttendanceService lectureAttendanceService)
    { 
        _studentService = studentService;
        _lectureService = lectureService;
        _courseService = courseService;
        _lectureAttendanceService = lectureAttendanceService;
    }

    // public IActionResult Index()
    // {
    //     var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
    //     Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;
    //
    //     StudentIndexDTO studentIndexDto = new StudentIndexDTO();
    //     
    //     List<LectureAttendance> lectureAttendanceForStudent = 
    //         _lectureAttendanceService.GetLectureAttendanceForStudent(student.StudentIndex).Result;
    //
    //     studentIndexDto.AlreadyAttendedLectures = lectureAttendanceForStudent;
    //
    //     List<StudentCourse> coursesForStudent = _studentService.GetCoursesForStudent(student.StudentIndex);
    //
    //     List<LectureCourses> upcomingLectureCourses = _lectureService.GetUpcomingLecturesForStudent(coursesForStudent);
    //     studentIndexDto.upcomingLectures = new List<Lecture>();
    //
    //     foreach (LectureCourses lc in upcomingLectureCourses)
    //     {
    //         studentIndexDto.upcomingLectures.Add(lc.Lecture);
    //     }
    //     
    //     return View(studentIndexDto);
    // }


    [HttpGet]
    [Authorize]
    public IActionResult RegisterAttendance(string id)
    {
        ErrorMessageDTO message = new ErrorMessageDTO();
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;

        // if (_lectureService.CheckIfLectureEnded(id, DateTime.Now))
        // {
        //     message.Message += "The lecture ended.";
        //     return View(message);
        // }
        //
        // if (!_lectureService.CheckIfLectureStarted(id, DateTime.Now))
        // {
        //     message.Message += "The lecture hasn't started yet.";
        //     return View(message);
        // }

        // List<long?> courseIds = _courseService.GetCoursesIdByLectureId(id);

        // bool inCourse = _studentService.CheckStudentInCourse(student.StudentIndex, courseIds);
        
        LectureAttendance attendance = _lectureAttendanceService.FindStudentRegistration(student.StudentIndex, id);
        //
        // if (!inCourse)
        // {
        //     message.Message += "Student is not enrolled in this course.";
        //     return View(message);
        // }
        //
        if (attendance != null)
        {
            message.Message += "\nYou are already registered for this lecture.";
            return View(message);
        }
        
        _lectureService.RegisterAttendance(student, id, DateTime.Now);
        return RedirectToAction("Index", "Home");
    }
}