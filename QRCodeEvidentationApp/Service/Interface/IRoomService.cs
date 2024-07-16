using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IRoomService
{
    /// <returns>Returns list of rooms.</returns>
    public Task<List<Room>> GetAllRooms();
}