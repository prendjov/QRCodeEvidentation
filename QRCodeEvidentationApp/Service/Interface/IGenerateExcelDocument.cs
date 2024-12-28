using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IGenerateExcelDocument
{
    public IActionResult GenerateDocument(List<AggregatedCourseAnalyticsDto> aggregatedCourseAnalytics, List<Lecture> lectures);
}