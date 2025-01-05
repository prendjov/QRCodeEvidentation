using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Service.Interface;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;

namespace QRCodeEvidentationApp.Service.Implementation;

public class GenerateExcelDocument : IGenerateExcelDocument
{
    private readonly ILectureGroupService _lectureGroupService;
    private readonly ILectureAttendanceService _lectureAttendanceService;
    private readonly ILectureService _lectureService;

    public GenerateExcelDocument(
        ILectureGroupService lectureGroupService,
        ILectureAttendanceService lectureAttendanceService,
        ILectureService lectureService
    )
    {
        _lectureGroupService = lectureGroupService;
        _lectureAttendanceService = lectureAttendanceService;
        _lectureService = lectureService;
    }
    
    public IActionResult GenerateDocument(Professor loggedInProfessor, string groupId)
    {
        
        List<Lecture> lectures = _lectureService.GetLecturesByProfessorAndCourseGroupId(loggedInProfessor.Id, groupId);
        HashSet<Student> students = new HashSet<Student>();

        foreach (Lecture l in lectures)
        {
            List<LectureAttendance> lectureAttendances = _lectureAttendanceService.GetLectureAttendance(l.Id).Result;
            foreach (LectureAttendance attendance in lectureAttendances)
            {
                students.Add(attendance.Student);
            }
        }

        List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalyticsDtos = new List<AggregatedCourseAnalyticsDto>();
        foreach (Student s in students)
        {
            AggregatedCourseAnalyticsDto singleAnalytic = new AggregatedCourseAnalyticsDto();
            singleAnalytic.Student = s;
            singleAnalytic.LectureAndAttendance = new Dictionary<string, long>();
            singleAnalytic.totalAttendances = 0;

            foreach (Lecture l in lectures)
            {
                singleAnalytic.LectureAndAttendance[l.Id] = 0;
            }
                
            List<LectureAttendance> attendances = _lectureAttendanceService.GetLectureAttendanceForStudent(s).Result;

            foreach (LectureAttendance attendance in attendances)
            {
                if (singleAnalytic.LectureAndAttendance.Keys.Contains(attendance.LectureId))
                {
                    singleAnalytic.LectureAndAttendance[attendance.LectureId] = 1;
                    singleAnalytic.totalAttendances += 1;
                }
            }
                
            aggregatedCourseAnalyticsDtos.Add(singleAnalytic);
        }
        
        if (aggregatedCourseAnalyticsDtos == null || aggregatedCourseAnalyticsDtos.Count == 0)
        {
            // Redirect to the DisplayError view with an error message
            var errorModel = new ErrorMessageDTO
            {
                Message = "There are no attendance records available for the selected course or lecture."
            };

            return new ViewResult
            {
                ViewName = "DisplayError",
                ViewData = new ViewDataDictionary<ErrorMessageDTO>(
                    new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                    new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                {
                    Model = errorModel
                }
            };
        }
        
        List<string> lectureNames = new List<string>();
        foreach (Lecture lecture in lectures)
        {
            lectureNames.Add(lecture.Title);
        }

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Lecture Attendance Report");
            
            // Add header
            worksheet.Cell(1, 1).Value = "Student Index";
            
            for (int i = 0; i < lectureNames.Count; i++)
            {
                worksheet.Cell(1, i + 2).Value = lectureNames[i];
            }
            worksheet.Cell(1, lectureNames.Count + 2).Value = "Total Attendance";

            // Add rows
            int currentRow = 2;
            foreach (AggregatedCourseAnalyticsDto analytic in aggregatedCourseAnalyticsDtos)
            {
                worksheet.Cell(currentRow, 1).Value = analytic.Student.StudentIndex;
                int columnNumber = 2;
                foreach (Lecture l in lectures)
                {
                    long presentInt = analytic.LectureAndAttendance[l.Id];
                    worksheet.Cell(currentRow, columnNumber).Value = presentInt;
                    columnNumber += 1;
                }

                worksheet.Cell(currentRow, columnNumber).Value = $"{analytic.totalAttendances} / {lectures.Count}";
                currentRow++;
            }

            // Adjust column widths
            worksheet.Columns().AdjustToContents();

            // Save the Excel document to a memory stream
            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "aggregated_data.xlsx"
                };
            }
        }
    }

    public IActionResult GenerateForSingleLecture(List<LectureAttendance> lectureAttends, Lecture lecture)
    {
        using (var workbook = new XLWorkbook())
        {
            // Create a worksheet
            var worksheet = workbook.Worksheets.Add("Lecture Attendance");

            // Add header row
            worksheet.Cell(1, 1).Value = "Index";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Last Name";
            worksheet.Cell(1, 4).Value = "Timestamp";

            // Format header row
            var headerRange = worksheet.Range(1, 1, 1, 4);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Populate data rows
            int row = 2; // Start from the second row
            foreach (var attendance in lectureAttends)
            {
                worksheet.Cell(row, 1).Value = attendance?.StudentIndex;
                worksheet.Cell(row, 2).Value = attendance?.Student?.Name;
                worksheet.Cell(row, 3).Value = attendance?.Student?.LastName;
                worksheet.Cell(row, 4).Value = attendance?.EvidentedAt.ToString();

                if (attendance?.EvidentedAt > lecture.ValidRegistrationUntil)
                {
                    // Apply a different style for late attendance
                    var lateRange = worksheet.Range(row, 1, row, 4);
                    lateRange.Style.Font.FontColor = XLColor.Red;
                }

                row++;
            }

            // Adjust column widths
            worksheet.Columns().AdjustToContents();
            
            // Save the Excel document to a memory stream
            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = lecture.Title + "_analytics.xlsx"
                };
            }
        }
    }
}
