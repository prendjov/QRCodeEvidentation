﻿namespace QRCodeEvidentationApp.Models
{
    public class LectureCourses
    {
        public string Id { get; set; } = null!;

        public string? LectureId { get; set; }

        public long? CourseId { get; set; }

        public virtual Lecture? Lecture { get; set; }

        public virtual Course? Course { get; set; }
    }
}
