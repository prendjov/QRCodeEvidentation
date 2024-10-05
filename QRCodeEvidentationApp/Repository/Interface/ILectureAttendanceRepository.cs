using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface ILectureAttendanceRepository
{
    /// <summary>
    /// Registers student as present
    /// </summary>
    /// <param name="lectureAttendance">Object with lectureId, studentId and datetime.</param>
    public void RegisterAttendance(LectureAttendance lectureAttendance);
    
    /// <param name="lectureId">Id of the lecture that you want to check attendance for.</param>
    /// <returns>List of students that have attended the specified lecture.</returns>
    public Task<List<LectureAttendance>> GetLectureAttendance(string? lectureId);
}