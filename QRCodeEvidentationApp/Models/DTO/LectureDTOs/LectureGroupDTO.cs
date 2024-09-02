namespace QRCodeEvidentationApp.Models.DTO
{
    public class LectureGroupDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public List<Course>? Courses { get; set; }
        public List<long> SelectedCourses { get; set; }
        public string? ProfessorId { get;set; }
    }
}
