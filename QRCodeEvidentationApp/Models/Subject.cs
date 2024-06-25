using System;
using System.Collections.Generic;

namespace FinkiEvidentationProject.Models;

public partial class Subject
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Semester { get; set; }

    public int? WeeklyAuditoriumClasses { get; set; }

    public int? WeeklyLabClasses { get; set; }

    public int? WeeklyLecturesClasses { get; set; }

    public string? Abbreviation { get; set; }

    public virtual ICollection<JoinedSubject> JoinedSubjects { get; set; } = new List<JoinedSubject>();

    public virtual ICollection<StudentSubjectEnrollment> StudentSubjectEnrollments { get; set; } = new List<StudentSubjectEnrollment>();

    public virtual ICollection<StudyProgramSubject> StudyProgramSubjects { get; set; } = new List<StudyProgramSubject>();

    public virtual SubjectDetail? SubjectDetail { get; set; }

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
}
