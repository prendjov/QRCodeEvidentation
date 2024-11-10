using Microsoft.AspNetCore.Mvc;

namespace QRCodeEvidentationApp.Service.Interface;

public interface IAnalytics<T>
{
    public FileContentResult GeneratePdf(List<T> data, string id);
}