using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QRCodeEvidentationApp.Models;

public class Student : IdentityUser
{
    [Required]
    public string StudentIndex { get; set; } = null!;
    
    public string? LastName { get; set; }

    public string? Name { get; set; }

    public string? ParentName { get; set; }

    public string? StudyProgramCode { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual StudyProgram? StudyProgramCodeNavigation { get; set; }
}
