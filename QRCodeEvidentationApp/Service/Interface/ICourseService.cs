using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

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
    
    /// <param name="teacherId">The id of the teacher.</param>
    /// <returns>Returns all the courses for a specific teacher id, doesn't matter if professor
    /// or assistant for the course.</returns>
    public List<Course> GetCourses(string? teacherId);

    /// <param name="courseId">The id of the course.</param>
    /// <param name="professorId">The id of the professor.</param>
    /// <returns>Boolean value true if the professor is assigned to the specified course, else returns false.</returns>
    public bool ProfessorAtCourse(long? courseId, string? professorId);

    /// <param name="courseId">The id of the course.</param>
    /// <returns>Course object.</returns>
    public Course GetCourse(long courseId);
}