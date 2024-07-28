using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;

namespace QRCodeEvidentationApp.Repository.Implementation;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Room> _entities;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<Room>();
    }
    
    public async Task<List<Room>> GetAllRooms()
    {
        return await _entities.ToListAsync();
    }

    public async Task<List<Room>> GetAvailableRoomsForDates(DateTime? startDate, DateTime? endDate)
    {
        if (startDate == null || endDate == null)
        {
            throw new ArgumentNullException("Start date and end date must be provided.");
        }

        return await _context.Rooms
            .Where(room => !room.Lectures
                .Any(lecture => lecture.StartsAt < endDate && lecture.EndsAt > startDate)).Include("Lectures")
            .ToListAsync();
    }
}