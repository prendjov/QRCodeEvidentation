namespace QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

public class AggregatedCourseAnalyticsDto
{
    public Student? Student { get; set; }
    
    public Dictionary<string, long> LectureAndAttendance { get; set; }

    public long totalAttendances { get; set; }
}