using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinkiEvidentationProject.Models;

public partial class Professor
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public short? OrderingRank { get; set; }

    public string? RoomName { get; set; }

    public virtual ICollection<Course> CourseAssistants { get; set; } = new List<Course>();

    public virtual ICollection<CourseProfessorEvaluation> CourseProfessorEvaluations { get; set; } = new List<CourseProfessorEvaluation>();

    public virtual ICollection<Course> CourseProfessors { get; set; } = new List<Course>();

    public virtual ProfessorDetail? ProfessorDetail { get; set; }

    public virtual ICollection<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; } = new List<StudentSubjectEnrollment>();

    public virtual ICollection<StudyProgramSubjectProfessor> StudyProgramSubjectProfessors { get; set; } = new List<StudyProgramSubjectProfessor>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
}
