namespace QRCodeEvidentationApp.Models.DTO.StudentDTO;

public class StudentIndexDTO
{
    public List<LectureAttendance> AlreadyAttendedLectures { get; set; }
    public List<Lecture> upcomingLectures { get; set; }
}