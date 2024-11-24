using System.Security.Claims;
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
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly ILectureService _lectureService;
        private readonly IProfessorService _professorService;
        private readonly ICourseService _courseService;
        private readonly IRoomService _roomService;
        private readonly ILectureGroupService _lectureGroupService;
        private readonly ILectureAttendanceService _lectureAttendanceService;

        public LecturesController(ILectureService lectureService, 
            IProfessorService professorService,
            ICourseService courseService,
            IRoomService roomService,
            ILectureGroupService lectureGroupService,
            ILectureAttendanceService lectureAttendanceService)
        {
            _lectureService = lectureService;
            _professorService = professorService;
            _courseService = courseService;
            _roomService = roomService;
            _lectureGroupService = lectureGroupService;
            _lectureAttendanceService = lectureAttendanceService;
        }

        // GET: Lecture
        public async Task<IActionResult> Index()
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
            if (isProfessor)
            {
                Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
                lectures = _lectureService.GetLecturesForProfessor(loggedInProfessor.Id);
            }

            return View(lectures);
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
            dto.AllRooms = _roomService.GetAllRooms().Result;
            dto.LecturesOnSpecificDate = _lectureService.FilterLectureByDateOrCourse(DateTime.Now, null, null);
            
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

            dto.CoursesProfessor = await _courseService.GetCoursesForProfessor(loggedInProfessor.Id);
            dto.CoursesAssistant = await _courseService.GetCoursesForAssistant(loggedInProfessor.Id);

            dto.Groups = await _lectureGroupService.ListByProfessor(loggedInProfessor.Id);

            dto.lecture = lecture;
            dto.lectureId = id;
            
            dto.AllRooms = _roomService.GetAllRooms().Result;
            dto.LecturesOnSpecificDate = _lectureService.FilterLectureByDateOrCourse(DateTime.Now, null, null);
            
            return View(dto);
        }

        // POST: Lecture/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, LectureEditDto lectureDto)
        {
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
                existingLecture.RoomName = lectureDto.lecture.RoomName;
                existingLecture.Type = lectureDto.lecture.Type;

                _lectureService.EditLecture(existingLecture);

                if(lectureDto.GroupCourseId != null)
                    _lectureService.AddLectureCoursesFromGroup(lectureDto.GroupCourseId, id);


                

                return RedirectToAction(nameof(Index));
            }
            return View(lectureDto);
        }
        
        // GET: Lecture/Delete/5
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = _lectureService.GetLectureById(id);

            return View(lecture);
        }

        // POST: Lecture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Invalid lecture ID." });
            }

            try
            {
                _lectureService.DeleteLecture(id);

                return Json(new { success = true, message = "Lecture deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult SelectDates()
        {
            LectureDto dto = new LectureDto();
            return View(dto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DisableLecture(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Invalid lecture ID." });
            }

            try
            {
                Lecture l = _lectureService.DisableLecture(id);
                return Json(new { success = true, message = "Lecture disabled successfully.", validRegistrationUntil = l.ValidRegistrationUntil});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GenerateQRCode(string id)
        {
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
        public IActionResult GetLectureAnalytics(string id)
        {
            List<LectureAttendance> lectureAttends = _lectureAttendanceService.GetLectureAttendance(id).Result;
            Lecture lecture = _lectureService.GetLectureById(id);
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
                                columns.ConstantColumn(50); // Student Index
                                columns.RelativeColumn();    // Student Name
                                columns.RelativeColumn();    // Attendance Status
                                columns.ConstantColumn(100); // Timestamp
                            });

                            // Add table header
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Index");
                                header.Cell().Element(CellStyle).Text("Name");
                                header.Cell().Element(CellStyle).Text("Last name");
                                header.Cell().Element(CellStyle).Text("Timestamp");
                            });

                            // Add table rows from lecture attendance data
                            foreach (var attendance in lectureAttends)
                            {
                                if (attendance?.EvidentedAt > lecture.ValidRegistrationUntil)
                                {
                                    table.Cell().Element(LateAttendanceStyle).Text(attendance?.StudentIndex);
                                    table.Cell().Element(LateAttendanceStyle).Text(attendance?.Student?.Name);
                                    table.Cell().Element(LateAttendanceStyle).Text(attendance?.Student?.LastName);
                                    table.Cell().Element(LateAttendanceStyle).Text(attendance?.EvidentedAt.ToString());
                                }
                                else
                                {
                                    table.Cell().Element(CellStyle).Text(attendance?.StudentIndex);
                                    table.Cell().Element(CellStyle).Text(attendance?.Student?.Name);
                                    table.Cell().Element(CellStyle).Text(attendance?.Student?.LastName);
                                    table.Cell().Element(CellStyle).Text(attendance?.EvidentedAt.ToString());
                                }
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
            return File(pdfBytes, "application/pdf", $"Lecture_{id}.pdf");
        }
    }
}
