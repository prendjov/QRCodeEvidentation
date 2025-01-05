namespace QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;


public class Attendance
{
    public long isAttended { get; set; }
    public bool isLate { get; set; }

    public Attendance(long isAttended, bool isLate)
    {
        this.isAttended = isAttended;
        this.isLate = isLate;
    }
}

public class AggregatedCourseAnalyticsDto
{
    public Student? Student { get; set; }
    
    public Dictionary<string, Attendance> LectureAndAttendance { get; set; }

    public long totalAttendances { get; set; }
}