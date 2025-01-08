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

}