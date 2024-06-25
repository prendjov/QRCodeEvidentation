using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class StudentCourse
{
    public long Id { get; set; }

    public string? StudentStudentIndex { get; set; }

    public long? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student{ get; set; }
}
