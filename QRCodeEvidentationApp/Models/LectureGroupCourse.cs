using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Models
{
    public class LectureGroupCourse
    {
        public string Id { get; set; } = null!;

        public string LectureGroupId { get; set; }

        public long CourseId { get; set; }

        public LectureGroup LectureGroup { get; set; }

        public Course Course { get; set; }
    }
}
