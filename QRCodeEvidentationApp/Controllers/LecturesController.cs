using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILectureService _lectureService;
        private readonly IProfessorService _professorService;
        private readonly ICourseService _courseService;
        private readonly IRoomService _roomService;

        public LecturesController(ILectureService lectureService, IProfessorService professorService,
            ICourseService courseService,
            IRoomService roomService)
        {
            _lectureService = lectureService;
            _professorService = professorService;
            _courseService = courseService;
            _roomService = roomService;
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
            bool isProfessor = User.IsInRole("Professor");
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
            dto.loggedInProfessorId = loggedInProfessor.Id;
            dto.StartsAt = DateTime.Parse(startsAt);
            dto.EndsAt = DateTime.Parse(endsAt);
            
            List<Room> availableRooms = await _roomService.GetAvailableRoomsForDates(dto.StartsAt, dto.EndsAt);

            if (availableRooms.Count == 0)
            {
                return RedirectToAction("SelectDates");
            }

            dto.GetAvailableRooms = availableRooms;
            
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

            List<Room> availableRooms = await _roomService.GetAllRooms();

            dto.lecture = lecture;
            dto.lectureId = id;
            dto.GetAvailableRooms = availableRooms;
            
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

                bool ValidRegistrationResult = _lectureService.CheckValidRegistrationDate(existingLecture.StartsAt, existingLecture.EndsAt, existingLecture.ValidRegistrationUntil);
                bool DateTimeInOrder = _lectureService.CheckStartAndEndDateTime(existingLecture.StartsAt, existingLecture.EndsAt);

                if (!DateTimeInOrder)
                {
                    var lectureEditDto = PopulateLectureEditDto(lectureDto, id, "StartsAt can't be after EndsAt or the lecture is scheduled for too long time range.");
                    return View(lectureEditDto);
                }

                if (!ValidRegistrationResult)
                {
                    var lectureEditDto = PopulateLectureEditDto(lectureDto, id, "Valid Registration Until is not in the scheduled lecture range.");
                    return View(lectureEditDto);
                }

                bool roomAvailability = _roomService.CheckRoomAvailability(existingLecture.StartsAt, existingLecture.EndsAt, existingLecture.RoomName, existingLecture.Id);

                if (!roomAvailability)
                {
                    var lectureEditDto = PopulateLectureEditDto(lectureDto, id, $"The specified room is not available on {existingLecture.StartsAt} - {existingLecture.EndsAt}.");
                    return View(lectureEditDto);
                }
             
                _lectureService.EditLecture(existingLecture);
                return RedirectToAction(nameof(Index));
            }
            return View(lectureDto);
        }

        private LectureEditDto PopulateLectureEditDto(LectureEditDto lectureDto, string id, string errMessage)
        {
            var lecture = _lectureService.GetLectureById(id);
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Professor loggedInProfessor = _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException()).Result;

            lectureDto.CoursesProfessor = _courseService.GetCoursesForProfessor(loggedInProfessor.Id).Result;
            lectureDto.CoursesAssistant = _courseService.GetCoursesForAssistant(loggedInProfessor.Id).Result;

            List<Room> availableRooms = _roomService.GetAllRooms().Result;

            lectureDto.lecture = lecture;
            lectureDto.lectureId = id;
            lectureDto.GetAvailableRooms = availableRooms;
            lectureDto.ErrMessage = errMessage;

            return lectureDto;
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
                _lectureService.DisableLecture(id);

                return Json(new { success = true, message = "Lecture disabled successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
