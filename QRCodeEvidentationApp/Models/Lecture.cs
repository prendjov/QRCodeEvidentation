namespace FinkiEvidentationProject.Models
{
    public class Lecture
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; }

        public DateTime StartsAt { get; set; }

        public string RoomName { get; set; }

        public string ProfessorId { get; set; }

        public virtual Room Room { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual ICollection<LectureCourses> Courses { get; set; } = new List<LectureCourses>();

    }
}