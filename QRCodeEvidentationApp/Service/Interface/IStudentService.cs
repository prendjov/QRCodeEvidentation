using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IStudentService
{
    /// <summary>
    /// Matches the logged in user with the student in Student table
    /// </summary>
    /// <param name="email">The email of the student. (We match by email)</param>
    /// <returns>A student object.</returns>
    public Task<Student> GetStudentFromUserEmail(string email);
    
    /// <summary>
    /// Checks if a given student is enrolled in some course.
    /// </summary>
    /// <param name="studentIndex">The index of the student</param>
    /// <param name="courseId">The ids of the courses</param>
    /// <returns>True if the student is in the courses, else False.</returns>
    public bool CheckStudentInCourse(string studentIndex, List<long?> courseId);
    
    public List<StudentCourse> GetStudentsForProfessor(string professorId);

    public List<StudentCourse> GetCoursesForStudent(string studentIndex);
}