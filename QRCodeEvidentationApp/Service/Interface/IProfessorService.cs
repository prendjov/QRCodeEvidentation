using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IProfessorService
{
    /// <summary>
    /// Matches the logged in user with the professor in Professor table
    /// </summary>
    /// <param name="email">The email of the professor. (We match by email)</param>
    /// <returns>A professor object.</returns>
    public Task<Professor> GetProfessorFromUserEmail(string email);
}