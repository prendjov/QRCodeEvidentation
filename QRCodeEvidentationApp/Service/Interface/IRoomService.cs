using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IRoomService
{
    /// <returns>Returns list of rooms.</returns>
    public Task<List<Room>> GetAllRooms();

    /// <summary>
    /// Takes the available rooms (rooms that don't have lecture scheduled) in the specified time period
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>List of available rooms.</returns>
    public Task<List<Room>> GetAvailableRoomsForDates(DateTime? startDate, DateTime? endDate);
}