using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class StudyProgramSubject
{
    public string Id { get; set; } = null!;

    public string? SubjectId { get; set; }

    public bool? Mandatory { get; set; }

    public short? Semester { get; set; }

    public double? Order { get; set; }

    public string? StudyProgramCode { get; set; }

    public string? DependenciesOverride { get; set; }

    public string? SubjectGroup { get; set; }

    public virtual StudyProgram? StudyProgramCodeNavigation { get; set; }

    public virtual ICollection<StudyProgramSubjectProfessor> StudyProgramSubjectProfessors { get; set; } = new List<StudyProgramSubjectProfessor>();

    public virtual Subject? Subject { get; set; }

    public virtual SubjectDetail? SubjectDetail { get; set; }
}
