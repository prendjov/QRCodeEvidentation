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

    
    /// <summary>
    /// Checks if the room is available for the given dates
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="roomName">The unique name of the room.</param>
    /// <param name="lectureId">The id of the lecture that we check for.</param>
    /// <returns>True if the room is not occupied on that datetime, else false.</returns>
    public bool CheckRoomAvailability(DateTime startDate, DateTime endDate, string roomName, string lectureId);
}