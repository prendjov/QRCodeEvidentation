using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILectureRepository _lectureRepository;

    public RoomService(IRoomRepository roomRepository, ILectureRepository lectureRepository)
    {
        _roomRepository = roomRepository;
        _lectureRepository = lectureRepository;
    }
    
    public async Task<List<Room>> GetAllRooms()
    {
        return await _roomRepository.GetAllRooms();
    }

    public async Task<List<Room>> GetAvailableRoomsForDates(DateTime? startDate, DateTime? endDate)
    {
        List<Room> allRooms = await _roomRepository.GetAllRooms();
        List<Lecture> lecturesOnDate = await _lectureRepository.FilterLectureByDateOrCourse(startDate, endDate, null);


        HashSet<Room> occupiedRooms = new HashSet<Room>();
        foreach (Lecture l in lecturesOnDate)
        {
            if (l.StartsAt > startDate && l.EndsAt < endDate)
            {
                if (l.Room != null) occupiedRooms.Add(l.Room);
            }
        }


        for (int i = allRooms.Count - 1; i >= 0; i--)
        {
            if (occupiedRooms.Contains(allRooms[i]))
            {
                allRooms.RemoveAt(i);
            }
        }

        return allRooms;
    }
}