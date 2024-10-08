using System.Linq.Expressions;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Implementation;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILectureCoursesRepository _lectureCoursesRepository;

    public CourseService(ICourseRepository courseRepository, ILectureCoursesRepository lectureCoursesRepository)
    {
        _courseRepository = courseRepository;
        _lectureCoursesRepository = lectureCoursesRepository;
    }

    public async Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId)
    {
        return await _courseRepository.GetCoursesForProfessor(professorId);
    }

    public async Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId)
    {
        return await _courseRepository.GetCoursesForAssistant(assistantId);
    }

    public List<long?> GetCoursesIdByLectureId(string? lectureId)
    {
        try
        {
            return _lectureCoursesRepository.GetCoursesForLecture(lectureId);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException();
        }
    }
}