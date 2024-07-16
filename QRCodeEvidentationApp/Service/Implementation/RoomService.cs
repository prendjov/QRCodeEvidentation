using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<List<Room>> GetAllRooms()
    {
        return await _roomRepository.GetAllRooms();
    }
}