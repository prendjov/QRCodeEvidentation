using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface ICourseRepository
{
    /// <summary>
    /// Takes the courses that the specified id is professor for.
    /// </summary>
    /// <param name="professorId">The id of the professor.</param>
    /// <returns>A list of courses for the professor.</returns>
    public Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId);
    
    /// <summary>
    /// Takes the courses that the specified id is assistant for.
    /// </summary>
    /// <param name="assistantId">The id of the assistant.</param>
    /// <returns>A list of courses for the assistant.</returns>
    public Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId);
}