using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class TeacherSubject
{
    public long Id { get; set; }

    public string? ProfessorId { get; set; }

    public string? SubjectId { get; set; }

    public virtual Professor? Professor { get; set; }

    public virtual Subject? Subject { get; set; }
}
