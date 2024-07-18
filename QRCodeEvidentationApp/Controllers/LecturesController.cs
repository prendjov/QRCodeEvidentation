using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create(LectureDto dto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
            
            dto.CoursesProfessor = await _courseService.GetCoursesForProfessor(loggedInProfessor.Id);
            dto.CoursesAssistant = await _courseService.GetCoursesForAssistant(loggedInProfessor.Id);
            
            List<Room> availableRooms = await _roomService.GetAvailableRoomsForDates(dto.StartsAt, dto.EndsAt);

            dto.GetAvailableRooms = availableRooms;
            
            return View(dto);
        }

        // POST: Lecture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(LectureDto dtoFilled)
        {
            if (ModelState.IsValid)
            {
                _lectureService.CreateLecture(new Lecture());
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Lecture/Edit/5
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecture = _lectureService.GetLectureById(id);
           
            return View(lecture);
        }

        // POST: Lecture/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,Title,StartsAt,RoomName,ProfessorId,Type,ValidRegistrationUntil")] Lecture lecture)
        {
            if (id != lecture.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _lectureService.EditLecture(lecture);
                
                return RedirectToAction(nameof(Index));
            }
            return View(lecture);
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
        public IActionResult DeleteConfirmed(string? id)
        {
            _lectureService.DeleteLecture(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SelectDates()
        {
        //     var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        //
        //     Professor loggedInProfessor = await _professorService.GetProfessorFromUserEmail(userEmail ?? throw new InvalidOperationException());
        //     
        //     LectureDto dto = new LectureDto();
        //     dto.CoursesProfessor = await _courseService.GetCoursesForProfessor(loggedInProfessor.Id);
        //     dto.CoursesAssistant = await _courseService.GetCoursesForAssistant(loggedInProfessor.Id);
        
            LectureDto dto = new LectureDto();

            return View(dto);
        }
        
        [HttpPost]
        public async Task<IActionResult> CheckRoomsAvailability(LectureDto lectureDto)
        {
            if (lectureDto == null || lectureDto.StartsAt == default || lectureDto.EndsAt == default)
            {
                return Json(new { isAvailable = false, rooms = new List<Room>() });
            }
            return RedirectToAction("Create", lectureDto);
        }
    }
}
