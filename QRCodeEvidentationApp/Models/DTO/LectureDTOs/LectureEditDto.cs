namespace QRCodeEvidentationApp.Models.DTO;

public class LectureEditDto
{
    public Lecture lecture { get; set; }
    public string lectureId { get; set; }
    
    public List<string> Type = new List<string>()
    {
        "Аудиториски", "Предавања"
    };
    
    // public List<Room>? GetAvailableRooms { get; set; }
    
    public List<CourseProfessor>? CoursesProfessor { get; set; }
    
    public List<LectureGroup>? Groups { get; set; }
    
    public long? CourseId { get; set; }
    
    public string? CourseProfessorId { get; set; }
    
    public string? CourseAssistantId { get; set; }
    
    public string? GroupCourseId { get; set; }
    public string? ErrMessage { get; set; }
}