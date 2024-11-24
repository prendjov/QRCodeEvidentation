using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface ILectureAttendanceService
{
    /// <param name="lectureId">The id of the lecture</param>
    /// <returns>Returns list of students that have attended the specified lecture.</returns>
    public Task<List<LectureAttendance>> GetLectureAttendance(string? lectureId);
    
    public Task<List<LectureAttendance>> GetLectureAttendanceForStudent(Student student);

    public LectureAttendance? FindStudentRegistration(string studentIndex, string lectureId);
}