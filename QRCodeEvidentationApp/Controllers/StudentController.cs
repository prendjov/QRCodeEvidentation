using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO.StudentDTO;
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

    public IActionResult Index()
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;

        StudentIndexDTO studentIndexDto = new StudentIndexDTO();
        
        List<LectureAttendance> lectureAttendanceForStudent = 
            _lectureAttendanceService.GetLectureAttendanceForStudent(student.StudentIndex).Result;

        studentIndexDto.AlreadyAttendedLectures = lectureAttendanceForStudent;

        List<StudentCourse> coursesForStudent = _studentService.GetCoursesForStudent(student.StudentIndex);

        List<LectureCourses> upcomingLectureCourses = _lectureService.GetUpcomingLecturesForStudent(coursesForStudent);
        studentIndexDto.upcomingLectures = new List<Lecture>();

        foreach (LectureCourses lc in upcomingLectureCourses)
        {
            studentIndexDto.upcomingLectures.Add(lc.Lecture);
        }
        
        return View(studentIndexDto);
    }


    [HttpGet]
    [Authorize]
    public IActionResult RegisterAttendance(string id)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        Student student = _studentService.GetStudentFromUserEmail(userEmail).Result;

        List<long?> courseIds = _courseService.GetCoursesIdByLectureId(id);

        bool inCourse = _studentService.CheckStudentInCourse(student.StudentIndex, courseIds);

        if (inCourse)
        {
            _lectureService.RegisterAttendance(student, id, DateTime.Now);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}