using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IGenerateExcelDocument
{
    /// <param name="loggedInProfessor">Object of the Professor (could be the logged in one, could be some other,
    /// depends on the use case.</param>
    /// <param name="groupId">Id of the Lecture/Course Group.</param>
    /// <summary>
    /// This function returns spreadsheet with analytics for some lecture group, where each row is student index and
    /// each column is the title of a lecture. At the end, in the generated spreadsheet, it can be seen at how many
    /// lectures some student attended, as well as in which lectures he/she attended. If some student registered
    /// late for the lecture, it's cell for that lecture will be with red background.
    /// </summary>
    /// <returns>Returns spreadsheet.</returns>
    public IActionResult GenerateDocument(Professor loggedInProfessor, string groupId);
    
    
    /// <param name="lectureAttends">List od LectureAttendance objects.</param>
    /// <param name="lecture">Lecture object.</param>
    /// <summary>
    /// This function returns spreadsheet with analytics for a specific lecture. It returns all the students
    /// that attended that lecture. The columns are, Index, Student Name, Student Lastname, and Timestamp when
    /// the student registered for the lecture. If the student registered too late, the font for that cell will be
    /// red.
    /// </summary>
    /// <returns>Returns spreadsheet.</returns>
    public IActionResult GenerateForSingleLecture(List<LectureAttendance> lectureAttends, Lecture lecture);
}