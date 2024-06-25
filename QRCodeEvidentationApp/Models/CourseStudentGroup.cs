using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class CourseStudentGroup
{
    public long Id { get; set; }
    public long CourseId { get; set; }

    public long StudentGroupsId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual StudentGroup StudentGroups { get; set; } = null!;
}
