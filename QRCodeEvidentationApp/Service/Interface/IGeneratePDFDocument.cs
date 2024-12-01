using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IGeneratePDFDocument
{
    public IActionResult GenerateDocument(List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalytics);
}