using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface IRoomRepository
{
    /// <returns>Returns list of rooms.</returns>
    public Task<List<Room>> GetAllRooms();
}