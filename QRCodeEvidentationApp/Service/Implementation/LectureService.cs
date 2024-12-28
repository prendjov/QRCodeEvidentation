using System.Globalization;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.Parsers;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;
using CsvHelper;
using CsvHelper.Configuration;

namespace QRCodeEvidentationApp.Service.Implementation;


public class LectureService : ILectureService
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ILectureCoursesRepository _lectureCourseRepository;
    private readonly ILectureAttendanceRepository _lectureAttendanceRepository;
    private readonly ILectureGroupRepository _lectureGroupRepository;

    public LectureService(ILectureRepository lectureRepository, ILectureCoursesRepository lectureCoursesRepository, ILectureAttendanceRepository lectureAttendanceRepository, ILectureGroupRepository lectureGroupRepository)
    {
        _lectureRepository = lectureRepository;
        _lectureCourseRepository = lectureCoursesRepository;
        _lectureAttendanceRepository = lectureAttendanceRepository;
        _lectureGroupRepository = lectureGroupRepository;
    }
    
    public List<Lecture> GetLecturesForProfessor(string? professorId)
    {
        return _lectureRepository.GetAllByProfessor(professorId).Result;
    }
    
    public Lecture GetLectureForProfessor(string? professorId)
    {
        return _lectureRepository.GetLectureByProfessorId(professorId).Result;
    }
    
    public Lecture GetLectureById(string? lectureId)
    {
        return _lectureRepository.GetLectureById(lectureId).Result;
    }

    public List<Lecture> FilterLectureByDateOrCourse(DateTime? dateFrom, DateTime? dateTo, List<long>? coursesIds)
    {
        return _lectureRepository.FilterLectureByDateOrCourse(dateFrom, dateTo, coursesIds).Result;
    }

    public Lecture EditLecture(Lecture lecture)
    {
        Lecture originalLecture = _lectureRepository.GetLectureById(lecture.Id).Result;

        List<LectureCourses> courses = originalLecture.Courses.ToList();

        foreach(LectureCourses course in courses)
        {
            _lectureCourseRepository.DeleteLectureCourse(course);
        }

        return _lectureRepository.UpdateLecture(lecture);
    }

    public List<Course> AddLectureCoursesFromGroup(string lectureGroupId, string lectureId)
    {
        LectureGroup group = _lectureGroupRepository.GetById(lectureGroupId).Result;
        Lecture lecture = _lectureRepository.GetLectureById(lectureId).Result;

        foreach (LectureGroupCourse course in group.Courses)
        {
            _lectureCourseRepository.CreateLectureCourse(new LectureCourses()
            {
                Id = Guid.NewGuid().ToString(),
                LectureId = lectureId,
                Lecture = lecture,
                Course = course.Course,
                CourseId= course.CourseId,
            });
        }

        return lecture.Courses.Select(c => c.Course).ToList();
    }

    public List<LectureCourses> GetUpcomingLecturesForStudent(List<StudentCourse> studentCourses)
    {
        return _lectureCourseRepository.GetUpcomingLecturesForStudent(studentCourses);
    }

    public bool CheckIfLectureEnded(string id, DateTime registrationTime)
    {
        Lecture lecture = _lectureRepository.GetLectureById(id).Result;

        if (lecture.EndsAt < registrationTime)
        {
            return true;
        }

        return false;
    }

    public bool CheckIfLectureStarted(string id, DateTime registrationTime)
    {
        Lecture lecture = _lectureRepository.GetLectureById(id).Result;

        if (lecture.StartsAt < registrationTime)
        {
            return true;
        }

        return false;
    }

    public List<Lecture> GetLecturesForProfessorPaginated(string? professorId, int page, int pageSize, out int totalLectures)
    {
        return _lectureRepository.GetLecturesForProfessorPaginated(professorId, page, pageSize, out totalLectures);
    }

    public Lecture DisableLecture(string? lectureId)
    {
        Lecture forDisabeling = _lectureRepository.GetLectureById(lectureId).Result;
        
        forDisabeling.ValidRegistrationUntil = DateTime.Now;

        return _lectureRepository.UpdateLecture(forDisabeling);
    }

    public Lecture DeleteLecture(string? lectureId)
    {
        var lecture = _lectureRepository.GetLectureById(lectureId).Result;
        return _lectureRepository.DeleteLecture(lecture).Result;
    }
    
    public void BulkInsertLectures(IFormFile csvFile)
    {
        // Open the CSV file
        using (var stream = csvFile.OpenReadStream())
        using (var reader = new StreamReader(stream))
        using (var csv = new CsvHelper.CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            // Read the records from the CSV file
            var records = csv.GetRecords<LectureCsvParser>().ToList();

            // Here, you can perform your bulk insert or process the records
            foreach (var record in records)
            {
                Lecture lecture = new Lecture
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = record.Title ?? string.Empty,
                    StartsAt = record.StartsAt,
                    EndsAt = record.EndsAt,
                    ProfessorId = record.ProfessorId,
                    Type = record.Type,
                    ValidRegistrationUntil = record.ValidRegistrationUntil
                };
                
                Lecture createdLecture = _lectureRepository.CreateNewLecture(lecture).Result;
                if (record.GroupCourseId != null)
                {
                    AddLectureCoursesFromGroup(record.GroupCourseId, lecture.Id);
                }
            }
        }
    }

    public List<Lecture> GetLecturesByProfessorAndCourseId(string? professorId, long? courseId)
    {
        return _lectureRepository.GetLecturesByProfessorAndCourseId(professorId, courseId);
    }

    public Lecture CreateLecture(LectureDto dtoFilled)
    { 
        Lecture lecture = new Lecture
        {
            Id = Guid.NewGuid().ToString(),
            Title = dtoFilled.Title ?? string.Empty,
            StartsAt = dtoFilled.StartsAt,
            EndsAt = dtoFilled.EndsAt,
            ProfessorId = dtoFilled.loggedInProfessorId,
            Type = dtoFilled.TypeSelected,
            ValidRegistrationUntil = dtoFilled.ValidRegistrationUntil
        };

        if (dtoFilled.CourseId.HasValue)
        {
            lecture.Courses.Add(_lectureCourseRepository.CreateLectureCourse(new LectureCourses
            {
                LectureId = lecture.Id,
                CourseId = dtoFilled.CourseId.Value,
            }));
        }

        Lecture createdLecture = _lectureRepository.CreateNewLecture(lecture).Result;
        if (dtoFilled.GroupCourseId != null)
        {
            AddLectureCoursesFromGroup(dtoFilled.GroupCourseId, lecture.Id);
        }
        return createdLecture;
    }

    public bool CheckValidRegistrationDate(DateTime startsAt, DateTime endsAt, DateTime? validRegistrationUntil)
    {
        if (validRegistrationUntil == null)
        {
            return true;
        }
        if (startsAt <= validRegistrationUntil && endsAt >= validRegistrationUntil)
        {
            return true;
        }

        return false;
    }

    public bool CheckStartAndEndDateTime(DateTime startsAt, DateTime endsAt)
    {
        if (endsAt < startsAt)
        {
            return false;
        }

        var timeDifference = endsAt - startsAt;
        if (timeDifference.Hours > 12)
        {
            return false;
        }

        return true;
    }

    public void RegisterAttendance(Student student, string? lectureId, DateTime evidentedAt)
    {

        LectureAttendance lectureAttendance = new LectureAttendance()
        {
            Id = Guid.NewGuid().ToString(),
            StudentIndex = student.StudentIndex,
            LectureId = lectureId,
            EvidentedAt = evidentedAt,
            Student = student
        };

        _lectureAttendanceRepository.RegisterAttendance(lectureAttendance);
    }
}