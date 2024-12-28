namespace QRCodeEvidentationApp.Models.DTO;

public class PaginatedLecturesViewModel
{
    public List<Lecture> Lectures { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalLectures { get; set; }
}