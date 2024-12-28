using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;

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
    public List<Lecture> FilterLectureByDateOrCourse(DateTime? dateFrom, DateTime? dateTo, List<long>? coursesIds);

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
    /// Delete the lecture with the specified identifier.
    /// </summary>
    /// <param name="lectureId">The identifier of the lecture to be deleted.</param>
    /// <returns>The delete lecture.</returns>
    public Lecture DeleteLecture(string? lectureId);

    /// <summary>
    /// Creates new lecture
    /// </summary>
    /// <param name="dtoFilled">The DTO from the lecture that should be created.</param>
    /// <returns>The created lecture.</returns>
    public Lecture CreateLecture(LectureDto dtoFilled);

    /// <summary>
    /// Checks whether the ValidRegistrationUntil field is within the StartsAt-EndsAt values for the lecture
    /// </summary>
    /// <param name="startsAt">When the lecture starts.</param>
    /// <param name="endsAt">When the lecture ends.</param>
    /// <param name="validRegistrationUntil">The value for validRegistrationUntil field.</param>
    /// <returns>True if the datetime in ValidRegistrationUntil is okay, else false.</returns>
    public bool CheckValidRegistrationDate(DateTime startsAt, DateTime endsAt, DateTime? validRegistrationUntil);


    /// <summary>
    /// Checks if the date time range in which the lecture is scheduled is okay.
    /// (Used to ensure if StartAt>EndsAt and other validations)
    /// </summary>
    /// <param name="startsAt">When the lecture starts.</param>
    /// <param name="endsAt">When the lecture ends.</param>
    /// <returns>True if the datetime for lecture is scheduled as it should, in the right order for datetime, else False.</returns>
    public bool CheckStartAndEndDateTime(DateTime startsAt, DateTime endsAt);

    /// <summary>
    /// Registers the student as present at the lecture.
    /// </summary>
    /// <param name="student">Student object.</param>
    /// <param name="lectureId">Unique identifier for the lecture.</param>
    /// <param name="evidentedAt">The time when the student was registered as present.</param>
    public void RegisterAttendance(Student student, string? lectureId, DateTime evidentedAt);

    /// <summary>
    /// Register new courses for a lecture by lecture group
    /// </summary>
    /// <param name="lectureGroupId">The Id of the Lecture Group</param>
    /// <param name="lectureId">The Lecture Id</param>
    /// <returns>The new courses that have been registered to the lecture</returns>
    public List<Course> AddLectureCoursesFromGroup(string lectureGroupId, string lectureId);

    public List<LectureCourses> GetUpcomingLecturesForStudent(List<StudentCourse> studentCourses);

    public bool CheckIfLectureEnded(string id, DateTime registrationTime);

    public bool CheckIfLectureStarted(string id, DateTime registrationTime);
    
    public List<Lecture> GetLecturesForProfessorPaginated(string? professorId, int page, int pageSize, out int totalLectures);

    public void BulkInsertLectures(IFormFile csvFile);

    public List<Lecture> GetLecturesByProfessorAndCourseId(string? professorId, long? courseId);
}