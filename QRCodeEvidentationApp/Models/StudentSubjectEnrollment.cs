using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class StudentSubjectEnrollment
{
    public string Id { get; set; } = null!;

    public string SemesterCode { get; set; } = null!;

    public string StudentStudentIndex { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public bool? Valid { get; set; }

    public string? InvalidNote { get; set; }

    public short? NumEnrollments { get; set; }

    public string? GroupName { get; set; }

    public long? GroupId { get; set; }

    public string? JoinedSubjectId { get; set; }

    public string? ProfessorId { get; set; }

    public string? Professors { get; set; }

    public string? Assistants { get; set; }

    public long? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual StudentGroup? StudentGroup { get; set; }

    public virtual JoinedSubject? JoinedSubject { get; set; }

    public virtual Professor? Professor { get; set; }

    public virtual Semester Semester { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
