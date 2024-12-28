namespace QRCodeEvidentationApp.Models.Parsers;

public class LectureCsvParser
{
    public string? Title { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? RoomId { get; set; }
    public DateTime ValidRegistrationUntil { get; set; }
    public string? ProfessorId { get; set; }
    public string? Type { get; set; }
    public string? GroupCourseId { get; set; }
}