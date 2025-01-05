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
}