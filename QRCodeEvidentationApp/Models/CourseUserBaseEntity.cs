namespace QRCodeEvidentationApp.Models;

public class CourseUserBaseEntity
{
    public string Id { get; set; }
    
    public long? CourseId { get; set; }
    
    public Course? Course { get; set; }
}