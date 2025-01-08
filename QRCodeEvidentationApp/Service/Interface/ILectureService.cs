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
    /// Edits Lecture record in database.
    /// </summary>
    /// <param name="lecture">Lecture object.</param>
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
    
    /// <param name="professorId">Id of professor.</param>
    /// <param name="page">Page number of the pagination.</param>
    /// <param name="pageSize">Long number of how many Lectures to be displayed on each page.</param>
    /// <param name="startsAtSorting">0 or 1 value. 0 is descending order, 1 is ascending.</param>
    /// <param name="lectureTypeFilter">Предавања or Аудиториски as Lecture type. It can also be empty string, meaning no type filter was applied.</param>
    /// <param name="totalLectures">Total Lectures created by the specified professor..</param>
    /// <returns>List of Lectures based on all the filters.</returns>
    public List<Lecture> GetLecturesForProfessorFiltered(string? professorId, int page, int pageSize, int startsAtSorting, string lectureTypeFilter, out int totalLectures);

    /// <param name="csvFile">IFormFile object with the data in csv. The CSV must have the following headers:
    /// Title, StartsAt, EndsAt, ValidRegistrationUntil, Type, GroupCourseId.</param>
    /// <param name="professorEmail">The email of the professor.</param>
    /// <returns>List of Lectures based on all the filters.</returns>
    public void BulkInsertLectures(IFormFile csvFile, string professorEmail);
    
    /// <param name="professorId">The id of the professor</param>
    /// <param name="courseGroupId">The id of the courseGroup.</param>
    /// <returns>List of Lectures by the specified professor for the specified course group.</returns>
    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId);

}