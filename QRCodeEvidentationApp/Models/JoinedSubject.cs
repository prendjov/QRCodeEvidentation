using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class JoinedSubject
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Codes { get; set; }

    public string? SemesterType { get; set; }

    public string? SubjectId { get; set; }

    public int? WeeklyLecturesClasses { get; set; }

    public int? WeeklyAuditoriumClasses { get; set; }

    public int? WeeklyLabClasses { get; set; }

    public string? Cycle { get; set; }

    public DateTime? LastUpdateTime { get; set; }

    public string? LastUpdateUser { get; set; }

    public string? ValidationMessage { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Subject? Subject { get; set; }

    public virtual ICollection<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; } = new List<StudentSubjectEnrollment>();
}
