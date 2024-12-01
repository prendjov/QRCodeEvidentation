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

    public async Task<List<LectureAttendance>> GetLectureAttendanceForStudent(Student student)
    {
        string? studentIndex = student.StudentIndex;

        return await _lectureAttendanceRepository.GetLectureAttendancesByStudent(studentIndex);
    }

    public LectureAttendance? FindStudentRegistration(string studentIndex, string lectureId)
    {
        return _lectureAttendanceRepository.FindStudentRegistration(studentIndex, lectureId);
    }
}