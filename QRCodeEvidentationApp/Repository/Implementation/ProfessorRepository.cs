using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class ProfessorRepository : IProfessorRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Professor> _entities;

    public ProfessorRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<Professor>();
    }

    public async Task<Professor> GetProfessorByAspUserEmail(string email)
    {
        return await _entities.SingleOrDefaultAsync(x => x.Email.Equals(email)) ?? throw new InvalidOperationException();
    }
}