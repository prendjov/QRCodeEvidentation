using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;

namespace FinkiEvidentationProject.Models;

public partial class StudyProgram
{
    [Key]
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<StudyProgramSubject> StudyProgramSubjects { get; set; } = new List<StudyProgramSubject>();
}
