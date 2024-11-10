using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface IStudentRepository
{
    /// <summary>
    /// Matches the logged in user with the student in Student table
    /// </summary>
    /// <param name="email">The email of the student. (We match by email)</param>
    /// <returns>A student object.</returns>
    public Task<Student> GetStudentByEmail(string email);

    public List<long?> GetCoursesForStudent(string index);
}