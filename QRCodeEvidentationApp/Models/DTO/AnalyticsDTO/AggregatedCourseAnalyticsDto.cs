namespace QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

public class AggregatedCourseAnalyticsDto
{
    public Student? Student { get; set; }
    
    public List<LectureAttendanceAnalyticDto>? LectureAttendance { get; set; }
}