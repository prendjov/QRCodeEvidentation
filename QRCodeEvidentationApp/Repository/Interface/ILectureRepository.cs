using QRCodeEvidentationApp.Models.Parsers;

namespace QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Models;

public interface ILectureRepository
{
    /// <summary>
    /// Retrieves all lectures associated with a specific professor.
    /// </summary>
    /// <param name="professorId">The identifier of the professor.</param>
    /// <returns>A list of lectures associated with the professor.</returns>
    public Task<List<Lecture>> GetAllByProfessor(string? professorId);

    /// <summary>
    /// Retrieves a lecture associated with a specific professor.
    /// </summary>
    /// <param name="professorId">The identifier of the professor.</param>
    /// <returns>The lecture associated with the professor.</returns>
    public Task<Lecture> GetLectureByProfessorId(string? professorId);

    /// <summary>
    /// Retrieves a lecture by its identifier.
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture.</param>
    /// <returns>The lecture with the specified identifier.</returns>
    public Task<Lecture> GetLectureById(string? lectureId);
    
    /// <summary>
    /// Updates the specified lecture.
    /// </summary>
    /// <param name="lecture">Lecture to be edited.</param>
    /// <returns>The edited lecture.</returns>
    public Lecture UpdateLecture(Lecture lecture);

    /// <summary>
    /// Delete the specified lecture.
    /// </summary>
    /// <param name="lecture">The lecture to be deleted.</param>
    /// <returns>The delete lecture.</returns>
    public Task<Lecture> DeleteLecture(Lecture lecture);

    /// <summary>
    /// Creates new lecture
    /// </summary>
    /// <param name="lecture">The lecture that should be created.</param>
    /// <returns>The created lecture.</returns>
    public Task<Lecture> CreateNewLecture(Lecture lecture);
    
    public List<Lecture> GetLecturesByIds(List<string> lectureIds);

    public List<Lecture> GetLecturesForProfessorFiltered(string professorId, int page, int pageSize, int startsAtSorting, string lectureTypeFilter,
        out int totalLectures);
    
    public List<Lecture> GetLecturesByProfessorAndCourseId(string? professorId, long? courseId);
    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId);

    public void BulkInsertLectures(List<LectureCsvParser> lectureCsvFormat, string professorEmail);
}