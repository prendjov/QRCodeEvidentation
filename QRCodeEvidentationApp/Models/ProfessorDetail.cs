using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QRCodeEvidentationApp.Models;

public partial class ProfessorDetail
{
    [Key]
    public string ProfessorId { get; set; } = null!;

    public double? Order { get; set; }

    public DateOnly? BirthDay { get; set; }

    public string? Degree { get; set; }

    public string? DegreeTitle { get; set; }

    public string? CurrentTitleId { get; set; }

    public virtual Professor Professor { get; set; } = null!;
}
