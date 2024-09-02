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
            if ((startDate >= l.StartsAt && l.EndsAt >= startDate) ||
                (endDate >= l.StartsAt && l.EndsAt >= endDate) || 
                (endDate >= l.EndsAt && startDate <= l.StartsAt))
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

    public bool CheckRoomAvailability(DateTime startDate, DateTime endDate, string roomName, string lectureId)
    {
        List<Lecture> lecturesOnDate = _lectureRepository.FilterLectureByDateOrCourse(startDate, endDate, null).Result;

        List<Lecture> roomInOccupiedFlag = new List<Lecture>();
        foreach (Lecture l in lecturesOnDate)
        {
            if ((startDate >= l.StartsAt && l.EndsAt >= startDate) ||
                (endDate >= l.StartsAt && l.EndsAt >= endDate) || 
                (endDate >= l.EndsAt && startDate <= l.StartsAt) && l.RoomName.Equals(roomName))
            {
                roomInOccupiedFlag.Add(l);
            }
        }

        foreach (Lecture l in roomInOccupiedFlag)
        {
            if (l.Id.Equals(lectureId))
            {
                return true;
            }
        }

        return false;
    }
}