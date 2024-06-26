using FinkiEvidentationProject.Models;

namespace QRCodeEvidentationApp.Models
{
    public class CourseProfessor
    {
        public string Id { get; set; }

        public string? ProfessorId {  get; set; }
        
        public long? CourseId { get; set; }

        public Professor? Professor { get; set; }

        public Course? Course { get; set; }
    }
}
