using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;

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

    /// <param name="teacherId">The id of the teacher.</param>
    /// <returns>Returns all the courses for a specific teahcer id, doesn't matter if professor
    /// or assistant for the course.</returns>
    public Task<List<Course>> GetCourses(string? teacherId);
    
    /// <param name="id">The id of the course.</param>
    /// <param name="teacherId">The id of the professor.</param>
    /// <returns>Returns CourseProfessor object.</returns>
    public Task<CourseProfessor?> GetCourseProfessorCombo(long? id, string? teacherId);

    /// <param name="id">The id of the course.</param>
    /// <param name="teacherId">The id of the professor.</param>
    /// <returns>Returns CourseAssistant object.</returns>
    public Task<CourseAssistant?> GetCourseAssistantCombo(long? id, string? teacherId);
    
    /// <param name="courseId">The id of the course.</param>
    /// <returns>Returns Course object.</returns>
    public Course Get(long courseId);
}