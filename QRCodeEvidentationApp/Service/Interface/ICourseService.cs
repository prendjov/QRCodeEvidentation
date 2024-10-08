using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface ICourseService
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
    
    /// <summary>
    /// Returns the courses ids for a given lectureid.
    /// </summary>
    /// <param name="lectureId">The id of the lecture.</param>
    /// <returns>List of course ids.</returns>
    public List<long?> GetCoursesIdByLectureId(string? lectureId);
}