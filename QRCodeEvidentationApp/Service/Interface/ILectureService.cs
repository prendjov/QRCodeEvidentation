using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface ILectureService
{
    /// <summary>
    /// Retrieves all lectures associated with a specific professor.
    /// </summary>
    /// <param name="professorId">The identifier of the professor.</param>
    /// <returns>A list of lectures associated with the professor.</returns>
    public List<Lecture> GetLecturesForProfessor(string? professorId);
    
    /// <summary>
    /// Retrieves a lecture associated with a specific professor.
    /// </summary>
    /// <param name="professorId">The identifier of the professor.</param>
    /// <returns>The lecture associated with the professor.</returns>
    public Lecture GetLectureForProfessor(string? professorId);
    
    /// <summary>
    /// Retrieves a lecture by its identifier (id).
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture.</param>
    /// <returns>The lecture with the specified identifier.</returns>
    public Lecture GetLectureById(string? lectureId);
    
    /// <summary>
    /// Filters lectures based on the specified date span or course identifiers.
    /// </summary>
    /// <param name="dateFrom">The date from which to start filtering lectures. If null, the earliest date available will be used.</param>
    /// <param name="dateTo">The date up to which the lectures are filtered. If null, the filter will include all lectures from dateFrom onwards.</param>
    /// <param name="coursesIds">A list of course identifiers to filter lectures. If null, the course filter is not applied.</param>
    /// <returns>A list of lectures that match the specified date span or course identifiers.</returns>
    public List<Lecture> FilterLectureByDateOrCourse(DateOnly? dateFrom, DateOnly? dateTo, List<long>? coursesIds);

    /// <summary>
    /// Edits the lecture with the specified identifier.
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture to be edited.</param>
    /// <returns>The edited lecture.</returns>
    public Lecture EditLecture(string? lectureId);

    /// <summary>
    /// Disables the lecture with the specified identifier.
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture to be disabled.</param>
    /// <returns>The disabled lecture.</returns>
    public Lecture DisableLecture(string? lectureId);
}