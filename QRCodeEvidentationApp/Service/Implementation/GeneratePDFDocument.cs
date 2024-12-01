using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Service.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QRCodeEvidentationApp.Service.Implementation;

public class GeneratePDFDocument : IGeneratePDFDocument
{
    private IContainer CellStyle(IContainer container)
    {
        return container.PaddingVertical(0).PaddingHorizontal(0).Border(1).BorderColor(Colors.Black);
    }
    
    public IActionResult GenerateDocument(List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalytics)
    {
        int lecturesNum = aggregatedCourseAnalytics[0].LectureAttendance.Count;
        List<LectureAttendanceAnalyticDto> lectures = aggregatedCourseAnalytics[0].LectureAttendance;

        List<string> lectureNames = new List<string>();
        foreach (LectureAttendanceAnalyticDto lecture in lectures)
        {
            lectureNames.Add(lecture.Lecture.Title);
        }
        
        // Generate the PDF in memory using QuestPDF
        byte[] pdfBytes;
        using (var memoryStream = new MemoryStream())
        {
            // Create the PDF document using QuestPDF's fluent API
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text("Lecture Attendance Report").Bold().FontSize(20);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < lecturesNum + 2; i++)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        // Add table header
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Student Index");
                            foreach (string s in lectureNames)
                            {
                                header.Cell().Element(CellStyle).Text(s);
                            }
                        });

                        // Add table rows from lecture attendance data
                        foreach (AggregatedCourseAnalyticsDto analytic in aggregatedCourseAnalytics)
                        {
                            table.Cell().Element(CellStyle).Text(analytic.Student.StudentIndex);
                            int numberAttendances = 0;
                            foreach (var element in analytic.LectureAttendance)
                            {
                                table.Cell().Element(CellStyle).Text(element.IsPresent);
                                if (element.IsPresent == 1)
                                {
                                    numberAttendances += 1;
                                }
                            }
                            table.Cell().Element(CellStyle).Text(numberAttendances + " / " + lecturesNum);
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on: ");
                            x.Span(DateTime.Now.ToString("yyyy-MM-dd"));
                        });
                });
            }).GeneratePdf(memoryStream);

            // Convert memory stream to a byte array
            pdfBytes = memoryStream.ToArray();
        }

        // Return the PDF as a downloadable file
        return new FileContentResult(pdfBytes, "application/pdf")
        {
            FileDownloadName = "aggregated_data.pdf"
        };
    }
}