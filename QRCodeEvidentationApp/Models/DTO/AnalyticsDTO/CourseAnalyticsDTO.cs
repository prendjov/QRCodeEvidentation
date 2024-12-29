namespace QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

public class CourseAnalyticsDTO
{
    // public List<CourseAnalyticsDTO> EachLectureAnalytics { get; set; }
    //
    // public string? lectureId;
    //
    // public Lecture lecture;
    //
    // public int NumberOfAttendees = 0;
    //
    // public int AtProfessorNumberOfAttendees = 0;
    //
    // public int NotAtProfessorNumberOfAttendees = 0;
    //
    // public long? courseId { get; set; }
    public Dictionary<Lecture, long> lecturesAndAttendees { get; set; }
    public long? courseId { get; set; }
}