using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Models
{
    public class CourseProfessor : CourseUserBaseEntity
    {
        public string? ProfessorId {  get; set; }
        
        public Professor? Professor { get; set; }
    }
}
