using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface IStudentCourseRepository
{
    bool CheckStudentCourse(string studentId, long? courseId);

    public List<StudentCourse> GetStudentsForProfessor(string professorId);
}