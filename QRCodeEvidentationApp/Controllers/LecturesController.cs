using System.Security.Claims;
using ClosedXML.Excel;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using QRCodeEvidentationApp.Service.Implementation;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace QRCodeEvidentationApp.Controllers
{
    [Authorize(Roles = "PROFESSOR")]
    public class LecturesController : Controller
    {
        private readonly ILectureService _lectureService;
        private readonly IProfessorService _professorService;
        private readonly ICourseService _courseService;
        private readonly ILectureGroupService _lectureGroupService;
        private readonly ILectureAttendanceService _lectureAttendanceService;

        public LecturesController(ILectureService lectureService, 
            IProfessorService professorService,
            ICourseService courseService,
            ILectureGroupService lectureGroupService,
            ILectureAttendanceService lectureAttendanceService)
        {
            _lectureService = lectureService;
            _professorService = professorService;
            _courseService = courseService;
            _lectureGroupService = lectureGroupService;
            _lectureAttendanceService = lectureAttendanceService;
        }

        // GET: Lecture
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            // take the logged in user
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
    
            // Get the logged in user's roles
            var userRoles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            List<Lecture> lectures = null;
            bool isProfessor = User.IsInRole("PROFESSOR");
            int totalLectures = 0; // Variable to store the total number of lectures

            if (isProfessor)
            {
                Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
        
                // Get the paginated lectures and total lecture count
                lectures = _lectureService.GetLecturesForProfessorPaginated(loggedInProfessor.Id, page, pageSize, out totalLectures);
            }

            // Calculate total pages based on total lectures and page size
            var totalPages = (int)Math.Ceiling(totalLectures / (double)pageSize);

            // Pass data to the view including pagination metadata
            var model = new PaginatedLecturesViewModel
            {
                Lectures = lectures,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalLectures = totalLectures
            };

            return View(model);
        }

        public IActionResult BulkAddLecturesView()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult BulkAddLecturesView(IFormFile csvFile)
        {
            if (csvFile != null && csvFile.Length > 0)
            {
                // PARSE THE CSV RECORDS AND ADD THE RECORDS IN THE DATABASE
                _lectureService.BulkInsertLectures(csvFile);
                return RedirectToAction("Index");
            }

            // If no file is uploaded, return to the same view with an error message
            ModelState.AddModelError("csvFile", "Please upload a CSV file.");
            return View();
        }

        // GET: Lecture/Details/5
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = _lectureService.GetLectureById(id);

            return View(lecture);
        }

        // GET: Lecture/Create
        [HttpGet]
        public async Task<IActionResult> CreateView(string startsAt, string endsAt)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            LectureDto dto = new LectureDto();
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());

            dto.CoursesProfessor = await _courseService.GetCoursesForProfessor(loggedInProfessor.Id);
            dto.CoursesAssistant = await _courseService.GetCoursesForAssistant(loggedInProfessor.Id);
            dto.Groups = await _lectureGroupService.ListByProfessor(loggedInProfessor.Id);
            dto.loggedInProfessorId = loggedInProfessor.Id;

            dto.StartsAt = DateTime.Now.Date;
            dto.EndsAt = DateTime.Now.Date;
            dto.ValidRegistrationUntil = DateTime.Now.Date;

            return View("Create", dto);
        }

        // POST: Lecture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LectureDto dtoFilled)
        {
            if (ModelState.IsValid)
            {
                _lectureService.CreateLecture(dtoFilled);
                return RedirectToAction(nameof(Index));
            }
            
            return View(dtoFilled);
        }

        // GET: Lecture/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = _lectureService.GetLectureById(id);
            LectureEditDto dto = new LectureEditDto();
            
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());

            Lecture lectureProfessor = _lectureService.GetLectureForProfessor(loggedInProfessor.Id);
            if (lectureProfessor == null)
            {
                return RedirectToAction(nameof(DisplayError), new { error = "The logged in professor doesn't have access to this lecture."});
            }

            dto.CoursesProfessor = await _courseService.GetCoursesForProfessor(loggedInProfessor.Id);
            dto.CoursesAssistant = await _courseService.GetCoursesForAssistant(loggedInProfessor.Id);

            dto.Groups = await _lectureGroupService.ListByProfessor(loggedInProfessor.Id);

            dto.lecture = lecture;
            dto.lectureId = id;
            
            return View(dto);
        }

        // POST: Lecture/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LectureEditDto lectureDto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());

            Lecture lectureProfessor = _lectureService.GetLectureForProfessor(loggedInProfessor.Id);
            if (lectureProfessor == null)
            {
                return RedirectToAction(nameof(DisplayError),
                    new { error = "The logged in professor doesn't have access to this lecture." });
            }

            if (id != lectureDto.lectureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingLecture = _lectureService.GetLectureById(id);
                
                if (existingLecture == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing lecture with the properties from the DTO
                existingLecture.Title = lectureDto.lecture.Title;
                existingLecture.StartsAt = lectureDto.lecture.StartsAt;
                existingLecture.EndsAt = lectureDto.lecture.EndsAt;
                existingLecture.ValidRegistrationUntil = lectureDto.lecture.ValidRegistrationUntil;
                existingLecture.Type = lectureDto.lecture.Type;

                _lectureService.EditLecture(existingLecture);

                if(lectureDto.GroupCourseId != null)
                    _lectureService.AddLectureCoursesFromGroup(lectureDto.GroupCourseId, id);
                return RedirectToAction(nameof(Index));
            }
            return View(lectureDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableLecture(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Invalid lecture ID." });
            }

            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
                Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());

                Lecture lectureProfessor = _lectureService.GetLectureForProfessor(loggedInProfessor.Id);
                if (lectureProfessor == null)
                {
                    return RedirectToAction(nameof(DisplayError), new { error = "The logged in professor doesn't have access to this lecture."});
                }
                
                Lecture l = _lectureService.DisableLecture(id);
                return Json(new { success = true, message = "Lecture disabled successfully.", validRegistrationUntil = l.ValidRegistrationUntil});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateQRCode(string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());

            Lecture lectureProfessor = _lectureService.GetLectureForProfessor(loggedInProfessor.Id);
            if (lectureProfessor == null)
            {
                return RedirectToAction(nameof(DisplayError), new { error = "The logged in professor doesn't have access to this lecture."});
            }
            
            // Directory to save the QR code image
            string directoryPath = "/home/vane/Desktop";
            string fileName = $"QRCode_{id}.png"; // Define the filename
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData =
                       qrGenerator.CreateQrCode("https://localhost:5097/Student/RegisterAttendance/" + id,
                           QRCodeGenerator.ECCLevel.Q))
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    // Get the QR code image as a byte array
                    byte[] qrCodeImage = qrCode.GetGraphic(20);

                    // Return the byte array as a file result
                    return File(qrCodeImage, "image/png");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public IActionResult DisplayError(string error)
        {
            ErrorMessageDTO errorMessageDto = new ErrorMessageDTO();
            errorMessageDto.Message = error;
            return View(errorMessageDto);
        }
        
        private IContainer CellStyle(IContainer container)
        {
            return container.PaddingVertical(0).PaddingHorizontal(0).Border(1).BorderColor(Colors.Black);
        }

        private IContainer LateAttendanceStyle(IContainer container)
        {
            return container
                .Background("#FF0000") // Set the background color to red
                .PaddingVertical(0)
                .PaddingHorizontal(0)
                .Border(1)
                .BorderColor("#000000");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetLectureAnalytics(string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());

            Lecture lectureProfessor = _lectureService.GetLectureForProfessor(loggedInProfessor.Id);
            if (lectureProfessor == null)
            {
                return RedirectToAction(nameof(DisplayError), new { error = "The logged-in professor doesn't have access to this lecture." });
            }

            List<LectureAttendance> lectureAttends = _lectureAttendanceService.GetLectureAttendance(id).Result;
            Lecture lecture = _lectureService.GetLectureById(id);

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

                using (var memoryStream = new MemoryStream())
                {
                    // Save workbook to memory stream
                    workbook.SaveAs(memoryStream);

                    // Convert memory stream to byte array
                    byte[] excelBytes = memoryStream.ToArray();

                    // Return Excel file
                    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Lecture_{id}.xlsx");
                }
            }
        }
    }
}
