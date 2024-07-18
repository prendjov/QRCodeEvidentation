namespace QRCodeEvidentationApp.Models.DTO;

public class LectureDto
{
    public List<Room>? GetAvailableRooms { get; set; }
    
    public string? RoomId { get; set; }

    public List<string> Type = new List<string>()
    {
        "Аудиториски", "Предавања"
    };
    
    public List<CourseProfessor>? CoursesProfessor { get; set; }
    
    public List<CourseAssistant>? CoursesAssistant { get; set; }
    
    public long? CourseId { get; set; }
    
    public long? CourseProfessorId { get; set; }
    
    public long? CourseAssistantId { get; set; }
    
    public DateTime StartsAt { get; set; }
    
    public DateTime EndsAt { get; set; }
    
    public DateTime ValidRegistrationUntil { get; set; }
    
    public string? Title { get; set; }
}