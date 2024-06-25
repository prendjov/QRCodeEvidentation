using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class CourseProfessorEvaluation
{
    public long Id { get; set; }

    public short Grade { get; set; }

    public long? CourseId { get; set; }

    public string? ProfessorId { get; set; }

    public string? Comment { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Professor? Professor { get; set; }
}
