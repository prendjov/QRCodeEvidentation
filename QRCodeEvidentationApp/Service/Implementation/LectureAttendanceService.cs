using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class LectureAttendanceService : ILectureAttendanceService
{
    private readonly ILectureAttendanceRepository _lectureAttendanceRepository;

    public LectureAttendanceService(ILectureAttendanceRepository lectureAttendanceRepository)
    {
        _lectureAttendanceRepository = lectureAttendanceRepository;
    }
    
    public Task<List<LectureAttendance>> GetLectureAttendance(string? lectureId)
    {
        return _lectureAttendanceRepository.GetLectureAttendance(lectureId);
    }

    public Task<List<LectureAttendance>> GetLectureAttendanceForStudent(string? studentId)
    {
        return _lectureAttendanceRepository.GetLectureByStudentId(studentId);
    }
}