using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Implementation;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId)
    {
        return await _courseRepository.GetCoursesForProfessor(professorId);
    }

    public async Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId)
    {
        return await _courseRepository.GetCoursesForAssistant(assistantId);
    }
}