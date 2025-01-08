using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Service.Interface;

public interface ILectureAttendanceService
{
    /// <param name="lectureId">The id of the lecture</param>
    /// <returns>Returns list of students that have attended the specified lecture.</returns>
    public Task<List<LectureAttendance>> GetLectureAttendance(string? lectureId);
    
    /// <param name="student">Student object</param>
    /// <returns>Returns all the lecture attendances for some student.</returns>
    public Task<List<LectureAttendance>> GetLectureAttendanceForStudent(Student student);

    /// <param name="studentIndex">The index of the student</param>
    /// <param name="lectureId">The id of the lecture</param>
    /// <returns>Returns single LectureAttendance object or null, depending on whether the student
    /// registered for the specified lecture.</returns>
    public LectureAttendance? FindStudentRegistration(string studentIndex, string lectureId);
}