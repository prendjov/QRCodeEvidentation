using QRCodeEvidentationApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QRCodeEvidentationApp.Models;

public partial class Professor
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public short? OrderingRank { get; set; }

    public string? RoomName { get; set; }

    public virtual ICollection<Course> CourseAssistants { get; set; } = new List<Course>();

    public virtual ICollection<Course> CourseProfessors { get; set; } = new List<Course>();

    public virtual ICollection<LectureGroup> ProfessorLectureGroups { get; set; } = new List<LectureGroup>();

    public virtual ProfessorDetail? ProfessorDetail { get; set; }
}
