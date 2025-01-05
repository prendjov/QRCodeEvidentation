using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Models
{
    public class LectureGroup
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; }

        public string ProfessorId { get; set; }

        public Professor Professor { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        
        public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
