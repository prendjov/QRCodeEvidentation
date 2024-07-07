using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        // GET: Lecture
        public IActionResult Index()
        {
            var professorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var lectures = _lectureService.GetLecturesForProfessor(professorId);
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,StartsAt,RoomName,ProfessorId,Type,ValidRegistrationUntil")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _lectureService.CreateLecture(lecture);
                return RedirectToAction(nameof(Index));
            }
            return View(lecture);
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
    }
}
