namespace QRCodeEvidentationApp.Models.DTO;

public class LectureEditDto
{
    public Lecture lecture { get; set; }
    public string lectureId { get; set; }
    
    public List<string> Type = new List<string>()
    {
        "Аудиториски", "Предавања"
    };
    
    public List<Room>? GetAvailableRooms { get; set; }
    
    public List<CourseProfessor>? CoursesProfessor { get; set; }
    
    public List<CourseAssistant>? CoursesAssistant { get; set; }

    public List<LectureGroup>? Groups { get; set; }
    
    public long? CourseId { get; set; }
    
    public string? CourseProfessorId { get; set; }
    
    public string? CourseAssistantId { get; set; }
    
    public string? GroupCourseId { get; set; }
    public string? ErrMessage { get; set; }
    
    // need these two fields to transfer the available rooms logic on the front-end
    public List<Room>? AllRooms { get; set; }
    public List<Lecture>? LecturesOnSpecificDate { get; set; }
}