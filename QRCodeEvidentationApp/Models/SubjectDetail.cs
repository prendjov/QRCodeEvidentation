using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinkiEvidentationProject.Models;

public partial class SubjectDetail
{
    [Key]
    public string SubjectId { get; set; } = null!;

    public short? DefaultSemester { get; set; }

    public string? AccreditationYear { get; set; }

    public short? ActivityPoints { get; set; }

    public string? Content { get; set; }

    public string? ContentEn { get; set; }

    public string? CopyOfSubjectDetailsId { get; set; }

    public float? Credits { get; set; }

    public string? Cycle { get; set; }

    public string? Dependencies { get; set; }

    public short? ExamPoints { get; set; }

    public string? ExerciseHours { get; set; }

    public string? GoalsDescription { get; set; }

    public string? GoalsDescriptionEn { get; set; }

    public string? HomeworkHours { get; set; }

    public string? Language { get; set; }

    public string? LearningMethods { get; set; }

    public string? LectureHours { get; set; }

    public string? NameEn { get; set; }

    public string? ProjectHours { get; set; }

    public short? ProjectPoints { get; set; }

    public string? SelfLearningHours { get; set; }

    public string? SignatureCondition { get; set; }

    public short? TestsPoints { get; set; }

    public string? TotalHours { get; set; }

    public string? QualityControl { get; set; }

    public bool? Placeholder { get; set; }

    public string? DependencyType { get; set; }

    public DateTime? LastUpdateTime { get; set; }

    public string? LastUpdateUser { get; set; }

    public virtual Subject Subject { get; set; } = null!;

    public virtual ICollection<StudyProgramSubject> StudyProgramSubjects { get; set; } = new List<StudyProgramSubject>();

}
