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
}