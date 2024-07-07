using QRCodeEvidentationApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRCodeEvidentationApp.Models;

public partial class Course
{
    public long Id { get; set; }

    public short? StudyYear { get; set; }

    public string? LastNameRegex { get; set; }

    public string? SemesterCode { get; set; }

    public int? NumberOfFirstEnrollments { get; set; }

    public int? NumberOfReEnrollments { get; set; }

    public float? GroupPortion { get; set; }

    public string? Groups { get; set; }

    public bool? English { get; set; }

    public virtual ICollection<CourseProfessor> CourseProfessors { get; set; } = new List<CourseProfessor>();

    public virtual ICollection<CourseAssistant> CourseAssistants { get; set; } = new List<CourseAssistant>();

    public virtual Semester? Semester { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

}
