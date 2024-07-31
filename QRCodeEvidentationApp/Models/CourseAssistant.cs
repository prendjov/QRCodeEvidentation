using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Models
{
    public class CourseAssistant : CourseUserBaseEntity
    {
        public string? AssistantId { get; set; }
        public Professor? Assistant { get; set; }
    }
}
