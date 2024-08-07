using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface;

public interface ILectureAttendanceRepository
{
    /// <summary>
    /// Registers student as present
    /// </summary>
    /// <param name="lectureAttendance">Object with lectureId, studentId and datetime.</param>
    public void RegisterAttendance(LectureAttendance lectureAttendance);
}