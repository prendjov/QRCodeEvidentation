using System.ComponentModel.DataAnnotations;

namespace QRCodeEvidentationApp.Models
{
    public class Lecture
    {
        [Key]
        public string Id { get; set; } = null!;

        public string Title { get; set; }

        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }

        // public string? RoomName { get; set; }

        public string? ProfessorId { get; set; }

        public string? Type { get; set; }

        public DateTime? ValidRegistrationUntil { get; set; }

        // public virtual Room? Room { get; set; }

        public virtual Professor? Professor { get; set; }
        
        public string? LectureGroupId {get; set;}
        
        public virtual LectureGroup? LectureGroup { get; set; }
    }
}