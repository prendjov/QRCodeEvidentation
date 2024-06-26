using FinkiEvidentationProject.Models;

namespace QRCodeEvidentationApp.Models
{
    public class CourseAssistant
    {
        public string Id { get; set; }

        public string? AssistantId { get; set; }

        public long? CourseId { get; set; }

        public Professor? Assistant { get; set; }

        public Course? Course { get; set; }
    }
}
