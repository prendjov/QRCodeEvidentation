using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface ILectureCoursesRepository
{
    /// <summary>
    /// Takes LectureCourse object ready to save in database
    /// </summary>
    /// <param name="lectureCourse">The object.</param>
    /// <returns>LectureCourse with id key, saved in the database.</returns>
    public LectureCourses CreateLectureCourse(LectureCourses lectureCourse);

    /// <summary>
    /// Deletes LectureCourse
    /// </summary>
    /// <param name="lectureCourse">The LectureCourse object that needs to be deleted</param>
    /// <returns>The deleted lecture course</returns>
    public LectureCourses DeleteLectureCourse(LectureCourses lectureCourse);

    /// <summary>
    /// Returns the course for the given lecture.
    /// </summary>
    /// <param name="lectureId">The lecture id for which we want the course.</param>
    /// <returns>The course associated with the given lecture.</returns>
    public List<long?> GetCoursesForLecture(string? lectureId);
}