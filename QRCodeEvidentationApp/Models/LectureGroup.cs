using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Models
{
    public class LectureGroup
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; }

        public string ProfessorId { get; set; }

        public Professor Professor { get; set; }
    }
}
