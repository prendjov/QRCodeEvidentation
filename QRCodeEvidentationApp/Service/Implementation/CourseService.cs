using System.Linq.Expressions;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Repository.Implementation;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILectureAttendanceRepository _lectureAttendanceRepository;

    public CourseService(ICourseRepository courseRepository,
        ILectureAttendanceRepository lectureAttendanceRepository)
    {
        _courseRepository = courseRepository;
        _lectureAttendanceRepository = lectureAttendanceRepository;
    }

    public async Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId)
    {
        return await _courseRepository.GetCoursesForProfessor(professorId);
    }

    public async Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId)
    {
        return await _courseRepository.GetCoursesForAssistant(assistantId);
    }
    

    public List<Course> GetCourses(string? teacherId)
    {
        return _courseRepository.GetCourses(teacherId).Result;
    }
    

    public bool ProfessorAtCourse(long? courseId, string? professorId)
    {
        var courseProfessor = _courseRepository.GetCourseProfessorCombo(courseId, professorId).Result;

        if (courseProfessor != null)
        {
            return true;
        }
        
        var courseAssistant = _courseRepository.GetCourseAssistantCombo(courseId, professorId).Result;

        if (courseAssistant != null)
        {
            return true;
        }

        return false;
    }

    public Course GetCourse(long courseId)
    {
        return _courseRepository.Get(courseId);
    }
}