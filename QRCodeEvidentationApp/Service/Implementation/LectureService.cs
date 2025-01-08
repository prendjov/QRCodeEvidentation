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
    private readonly ILectureAttendanceRepository _lectureAttendanceRepository;

    public LectureService(ILectureRepository lectureRepository, ILectureAttendanceRepository lectureAttendanceRepository)
    {
        _lectureRepository = lectureRepository;
        _lectureAttendanceRepository = lectureAttendanceRepository;
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

    public Lecture EditLecture(Lecture lecture)
    {
        Lecture originalLecture = _lectureRepository.GetLectureById(lecture.Id).Result;

        return _lectureRepository.UpdateLecture(lecture);
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

    public List<Lecture> GetLecturesForProfessorFiltered(string? professorId, int page, int pageSize, int startsAtSorting, string lectureTypeFilter, out int totalLectures)
    {
        return _lectureRepository.GetLecturesForProfessorFiltered(professorId, page, pageSize, startsAtSorting, lectureTypeFilter, out totalLectures);
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
    
    public void BulkInsertLectures(IFormFile csvFile, string professorEmail)
    {
        // Open the CSV file
        using (var stream = csvFile.OpenReadStream())
        using (var reader = new StreamReader(stream))
        using (var csv = new CsvHelper.CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            // Read the records from the CSV file
            var records = csv.GetRecords<LectureCsvParser>().ToList();

            _lectureRepository.BulkInsertLectures(records, professorEmail);
        }
    }

    public List<Lecture> GetLecturesByProfessorAndCourseGroupId(string? professorId, string courseGroupId)
    {
        return _lectureRepository.GetLecturesByProfessorAndCourseGroupId(professorId, courseGroupId);
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
            ValidRegistrationUntil = dtoFilled.ValidRegistrationUntil,
            LectureGroupId = dtoFilled.GroupCourseId
        };
        
        Lecture createdLecture = _lectureRepository.CreateNewLecture(lecture).Result;

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