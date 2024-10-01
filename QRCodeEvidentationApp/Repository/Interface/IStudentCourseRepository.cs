namespace QRCodeEvidentationApp.Repository.Interface;

public interface IStudentCourseRepository
{
    bool CheckStudentCourse(string studentId, long? courseId);
}