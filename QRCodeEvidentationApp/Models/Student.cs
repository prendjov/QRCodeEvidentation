﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinkiEvidentationProject.Models;

public partial class Student
{
    [Key]
    public string StudentIndex { get; set; } = null!;

    public string? Email { get; set; }

    public string? LastName { get; set; }

    public string? Name { get; set; }

    public string? ParentName { get; set; }

    public string? StudyProgramCode { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; } = new List<StudentSubjectEnrollment>();

    public virtual StudyProgram? StudyProgramCodeNavigation { get; set; }
}
