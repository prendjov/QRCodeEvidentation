using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;

namespace QRCodeEvidentationApp.Service.Interface;

public interface ILectureService
{
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
    /// Edits the lecture with the specified identifier.
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture to be edited.</param>
    /// <returns>The edited lecture.</returns>
    public Lecture EditLecture(Lecture lecture);

    /// <summary>
    /// Disables the lecture with the specified identifier.
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture to be disabled.</param>
    /// <returns>The disabled lecture.</returns>
    public Lecture DisableLecture(string? lectureId);

    /// <summary>
    /// Creates new lecture
    /// </summary>
    /// <param name="dtoFilled">The DTO from the lecture that should be created.</param>
    /// <returns>The created lecture.</returns>
    public Lecture CreateLecture(LectureDto dtoFilled);


    /// <summary>
    /// Registers the student as present at the lecture.
    /// </summary>
    /// <param name="student">Student object.</param>
    /// <param name="lectureId">Unique identifier for the lecture.</param>
    /// <param name="evidentedAt">The time when the student was registered as present.</param>
    public void RegisterAttendance(Student student, string? lectureId, DateTime evidentedAt);

    
    public List<Lecture> GetLecturesForProfessorFiltered(string? professorId, int page, int pageSize, int startsAtSorting, string lectureTypeFilter, out int totalLectures);

    public void BulkInsertLectures(IFormFile csvFile);

    public List<Lecture> GetLecturesByProfessorAndCourseId(string? professorId, long? courseId);
    
    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId);

}