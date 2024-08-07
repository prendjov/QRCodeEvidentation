using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class LectureService : ILectureService
{
    private readonly ILectureRepository _lectureRepository;
    private readonly ILectureCoursesRepository _lectureCourseRepository;
    private readonly ILectureAttendanceRepository _lectureAttendanceRepository;

    public LectureService(ILectureRepository lectureRepository, ILectureCoursesRepository lectureCoursesRepository, ILectureAttendanceRepository lectureAttendanceRepository)
    {
        _lectureRepository = lectureRepository;
        _lectureCourseRepository = lectureCoursesRepository;
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

    public List<Lecture> FilterLectureByDateOrCourse(DateTime? dateFrom, DateTime? dateTo, List<long>? coursesIds)
    {
        return _lectureRepository.FilterLectureByDateOrCourse(dateFrom, dateTo, coursesIds).Result;
    }

    public Lecture EditLecture(Lecture lecture)
    {
        return _lectureRepository.UpdateLecture(lecture);
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

    public Lecture CreateLecture(LectureDto dtoFilled)
    { 
        // Create the lecture entity from the DTO
        Lecture lecture = new Lecture
        {
            Id = Guid.NewGuid().ToString(), // Assuming you want to generate a new ID
            Title = dtoFilled.Title ?? string.Empty,
            StartsAt = dtoFilled.StartsAt,
            EndsAt = dtoFilled.EndsAt,
            RoomName = dtoFilled.RoomId,
            ProfessorId = dtoFilled.loggedInProfessorId,
            Type = dtoFilled.TypeSelected,
            ValidRegistrationUntil = dtoFilled.ValidRegistrationUntil
        };

        // Add the courses related to the lecture
        if (dtoFilled.CourseId.HasValue)
        {
            lecture.Courses.Add(_lectureCourseRepository.CreateLectureCourse(new LectureCourses
            {
                LectureId = lecture.Id,
                CourseId = dtoFilled.CourseId.Value,
            }));
        }
        return _lectureRepository.CreateNewLecture(lecture).Result;
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

    public void RegisterAttendance(string? studentIndex, string? lectureId, DateTime evidentedAt)
    {

        LectureAttendance lectureAttendance = new LectureAttendance()
        {
            Id = Guid.NewGuid().ToString(),
            StudentIndex = studentIndex,
            LectureId = lectureId,
            EvidentedAt = evidentedAt
        };

        _lectureAttendanceRepository.RegisterAttendance(lectureAttendance);
    }
}