using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class StudyProgramSubjectProfessor
{
    public string Id { get; set; } = null!;

    public string? StudyProgramSubjectId { get; set; }

    public string? ProfessorId { get; set; }

    public double? Order { get; set; }

    public virtual Professor? Professor { get; set; }

    public virtual StudyProgramSubject? StudyProgramSubject { get; set; }
}
