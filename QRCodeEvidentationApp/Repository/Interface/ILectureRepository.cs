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
    
    /// <param name="lectureIds">List of lecture ids.</param>
    /// <returns>List of the specified Lectures in the argument.</returns>
    public List<Lecture> GetLecturesByIds(List<string> lectureIds);
    
    /// <param name="professorId">Id of professor.</param>
    /// <param name="page">Page number of the pagination.</param>
    /// <param name="pageSize">Long number of how many Lectures to be displayed on each page.</param>
    /// <param name="startsAtSorting">0 or 1 value. 0 is descending order, 1 is ascending.</param>
    /// <param name="lectureTypeFilter">Предавања or Аудиториски as Lecture type. It can also be empty string, meaning no type filter was applied.</param>
    /// <param name="totalLectures">Total Lectures created by the specified professor..</param>
    /// <returns>List of Lectures based on all the filters.</returns>
    public List<Lecture> GetLecturesForProfessorFiltered(string professorId, int page, int pageSize, int startsAtSorting, string lectureTypeFilter,
        out int totalLectures);
    
    /// <param name="professorId">Id of professor.</param>
    /// <param name="courseGroupId">Page number of the pagination.</param>
    /// <returns>Returns all the lectures that are related with the specified professor id and the specified
    /// course id.</returns>
    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId);

    /// <param name="lectureCsvFormat">List of LectureCsvParser objects, each object contains data for new
    /// Lecture record.</param>
    /// <param name="professorEmail">Email of professor with who the Lectures will be in relation.</param>
    /// <summary>Adds Lectures in bulk.</summary>
    public void BulkInsertLectures(List<LectureCsvParser> lectureCsvFormat, string professorEmail);
}