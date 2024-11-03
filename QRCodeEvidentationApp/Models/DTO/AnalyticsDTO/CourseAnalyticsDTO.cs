namespace QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

public class CourseAnalyticsDTO
{
    public List<CourseAnalyticsDTO> EachLectureAnalytics { get; set; }

    public string? lectureId;
    
    public Lecture lecture;

    public int NumberOfAttendees = 0;

    public int AtProfessorNumberOfAttendees = 0;

    public int NotAtProfessorNumberOfAttendees = 0;
}