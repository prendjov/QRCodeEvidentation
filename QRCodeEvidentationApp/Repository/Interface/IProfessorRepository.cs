using NuGet.Protocol.Plugins;
using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface IProfessorRepository
{
    /// <summary>
    /// Matches the logged in user with the professor in Professor table
    /// </summary>
    /// <param name="email">The email of the professor. (We match by email)</param>
    /// <returns>A professor object.</returns>
    public Task<Professor> GetProfessorByAspUserEmail(string email);
    /// <summary>
    /// Get professor by Id
    /// </summary>
    /// <param name="id">Id for the searched professor</param>
    /// <returns>The professor with the searched Id</returns>
    public Task<Professor> GetById(string id);
}