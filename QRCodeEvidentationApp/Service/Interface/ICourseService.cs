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
    
    /// <summary>
    /// Returns the courses ids for a given lectureid.
    /// </summary>
    /// <param name="lectureId">The id of the lecture.</param>
    /// <returns>List of course ids.</returns>
    public List<long?> GetCoursesIdByLectureId(string? lectureId);
    
    /// <param name="teacherId">The id of the teacher.</param>
    /// <returns>Returns all the courses for a specific teacher id, doesn't matter if professor
    /// or assistant for the course.</returns>
    public List<Course> GetCourses(string? teacherId);

    public List<string?> GetLectureForCourseId(long? courseId);
    
    public CourseAnalyticsDTO GetCourseStatistics(List<Lecture> lecturesForProfessor, List<StudentCourse> studentCourses, List<string?> lecturesForCourse);
    
    public List<Lecture> GetLecturesForCourseId(long? courseId);

    public bool ProfessorAtCourse(long? courseId, string? professorId);
}