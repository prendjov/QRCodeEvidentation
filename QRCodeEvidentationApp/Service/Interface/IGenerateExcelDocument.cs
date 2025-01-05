using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IGenerateExcelDocument
{
    public IActionResult GenerateDocument(Professor loggedInProfessor, string groupId);

    public IActionResult GenerateForSingleLecture(List<LectureAttendance> lectureAttends, Lecture lecture);
}