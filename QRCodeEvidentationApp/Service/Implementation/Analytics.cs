// using Microsoft.AspNetCore.Mvc;
// using QRCodeEvidentationApp.Service.Interface;
// using QRCoder;
// using QuestPDF.Fluent;
// using QuestPDF.Helpers;
// using QuestPDF.Infrastructure;
// namespace QRCodeEvidentationApp.Service.Implementation;
//
// public class Analytics<T> : IAnalytics<T>
// {
//     
//     private IContainer CellStyle(IContainer container)
//     {
//         return container.PaddingVertical(0).PaddingHorizontal(0).Border(1).BorderColor(Colors.Black);
//     }
//     
//     
//     // Trying to abstract the process of generating PDF (might not be possible to be this abstract)
//     public FileContentResult GeneratePdf(List<T> data, string id)
//     {
//         // Generate the PDF in memory using QuestPDF
//             byte[] pdfBytes;
//             using (var memoryStream = new MemoryStream())
//             {
//                 // Create the PDF document using QuestPDF's fluent API
//                 Document.Create(container =>
//                 {
//                     container.Page(page =>
//                     {
//                         page.Header().Text("Lecture Attendance Report").Bold().FontSize(20);
//
//                         page.Content().Table(table =>
//                         {
//                             table.ColumnsDefinition(columns =>
//                             {
//                                 columns.ConstantColumn(50); // Student Index
//                                 columns.RelativeColumn();    // Student Name
//                                 columns.RelativeColumn();    // Attendance Status
//                                 columns.ConstantColumn(100); // Timestamp
//                             });
//                             
//                             // Add table header
//                             table.Header(header =>
//                             {
//                                 header.Cell().Element(CellStyle).Text("Index");
//                                 header.Cell().Element(CellStyle).Text("Name");
//                                 header.Cell().Element(CellStyle).Text("Last name");
//                                 header.Cell().Element(CellStyle).Text("Timestamp");
//                             });
//                             // Try to make this in JSON and iterate over key value, and populate it based on the name
//                             // of the keys
//                             // Add table rows from lecture attendance data
//                             foreach (var attendance in data)
//                             {
//                                 table.Cell().Element(CellStyle).Text(attendance?.Student.Name);
//                                 table.Cell().Element(CellStyle).Text(attendance?.Student?.Name);
//                                 table.Cell().Element(CellStyle).Text(attendance?.Student?.LastName);
//                                 table.Cell().Element(CellStyle).Text(attendance?.EvidentedAt.ToString());
//                             }
//                         });
//
//                         page.Footer()
//                             .AlignCenter()
//                             .Text(x =>
//                             {
//                                 x.Span("Generated on: ");
//                                 x.Span(DateTime.Now.ToString("yyyy-MM-dd"));
//                             });
//                     });
//                 }).GeneratePdf(memoryStream);
//
//                 // Convert memory stream to a byte array
//                 pdfBytes = memoryStream.ToArray();
//             }
//
//             // Return the PDF as a downloadable file
//             return new FileContentResult(pdfBytes, "application/pdf")
//             {
//                 FileDownloadName =
//                     $"LectureAttendanceReport_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf" // Use a more dynamic file name
//             };
//     }
// }