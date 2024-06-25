using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinkiEvidentationProject.Models;

public partial class Course
{
    public long Id { get; set; }

    public short? StudyYear { get; set; }

    public string? LastNameRegex { get; set; }

    public string? SemesterCode { get; set; }

    public string? JoinedSubjectId { get; set; }

    public string? ProfessorId { get; set; }

    [ForeignKey("ProfessorId")]
    public virtual Professor? Professor { get; set; }

    public string? AssistantId { get; set; }

    [ForeignKey("AssistantId")]
    public virtual Professor? Assistant { get; set; }

    public int? NumberOfFirstEnrollments { get; set; }

    public int? NumberOfReEnrollments { get; set; }

    public float? GroupPortion { get; set; }

    public string? Professors { get; set; }

    public string? Assistants { get; set; }

    public string? Groups { get; set; }

    public bool? English { get; set; }


    public virtual ICollection<CourseProfessorEvaluation> CourseProfessorEvaluations { get; set; } = new List<CourseProfessorEvaluation>();

    public virtual JoinedSubject? JoinedSubject { get; set; }

    public virtual Semester? Semester { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; } = new List<StudentSubjectEnrollment>();
}
