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
    public IActionResult GenerateDocument(List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalytics, List<Lecture> lectures)
    {
        if (aggregatedCourseAnalytics == null || aggregatedCourseAnalytics.Count == 0)
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
            foreach (AggregatedCourseAnalyticsDto analytic in aggregatedCourseAnalytics)
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
}
