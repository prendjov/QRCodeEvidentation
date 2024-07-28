namespace QRCodeEvidentationApp.Models.DTO;

public class LectureDto
{
    public string? RoomId { get; set; }

    public List<string>? Type = new List<string>()
    {
        "Аудиториски", "Предавања"
    };
    
    public string? TypeSelected { get; set; }
    
    public List<CourseProfessor>? CoursesProfessor { get; set; }
    
    public List<CourseAssistant>? CoursesAssistant { get; set; }
    
    public long? CourseId { get; set; }
    
    public long? CourseProfessorId { get; set; }
    
    public long? CourseAssistantId { get; set; }
    
    public DateTime StartsAt { get; set; }
    
    public DateTime EndsAt { get; set; }
    
    public DateTime ValidRegistrationUntil { get; set; }
    
    public string? Title { get; set; }
    
    public string? loggedInProfessorId { get; set; }
    
    public string? ErrMessage { get; set; }
    
    // need these two fields to transfer the available rooms logic on the front-end
    public List<Room>? AllRooms { get; set; }
    public List<Lecture>? LecturesOnSpecificDate { get; set; }
}