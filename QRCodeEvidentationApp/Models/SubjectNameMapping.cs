using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class SubjectNameMapping
{
    public long Id { get; set; }

    public string? OldName { get; set; }

    public string? SubjectId { get; set; }
}
