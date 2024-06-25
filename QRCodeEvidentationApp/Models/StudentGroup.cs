using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class StudentGroup
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public short? StudyYear { get; set; }

    public string? LastNameRegex { get; set; }

    public string? SemesterCode { get; set; }

    public string? Programs { get; set; }

    public bool? English { get; set; }

    public virtual Semester? SemesterCodeNavigation { get; set; }

    public virtual ICollection<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; } = new List<StudentSubjectEnrollment>();
}
