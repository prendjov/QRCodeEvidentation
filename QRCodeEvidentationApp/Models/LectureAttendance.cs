namespace FinkiEvidentationProject.Models
{
    public class LectureAttendance
    {
        public string Id { get; set; } = null!;

        public string? LectureId { get; set; }

        public string? StudentIndex { get; set; }

        public virtual Lecture? Lecture { get; set; }

        public virtual Student? Student { get; set; }

        public DateTime EvidentedAt { get; set; }
    }
}
