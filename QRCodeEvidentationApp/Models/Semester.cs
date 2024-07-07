using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QRCodeEvidentationApp.Models;

public partial class Semester
{
    [Key]
    public string Code { get; set; } = null!;

    public string? SemesterType { get; set; }

    public string? Year { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? EnrollmentStartDate { get; set; }

    public DateOnly? EnrollmentEndDate { get; set; }

    public string? State { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

}
