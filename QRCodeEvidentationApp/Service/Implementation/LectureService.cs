using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class LectureService : ILectureService
{
    private readonly ILectureRepository _lectureRepository;

    public LectureService(ILectureRepository lectureRepository)
    {
        _lectureRepository = lectureRepository;
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

    public Lecture CreateLecture(Lecture lecture)
    { 
        return _lectureRepository.CreateNewLecture(lecture).Result;
    }
}