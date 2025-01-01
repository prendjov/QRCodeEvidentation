namespace QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

public class CourseGroupAnalyticsDTO
{
    public Dictionary<Lecture, long> lecturesAndAttendees { get; set; }
    public string courseGroupId { get; set; }
}