using Microsoft.AspNetCore.Authorization;
using NuGet.Packaging;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Repository.Implementation;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QRCodeEvidentationApp.Service.Implementation
{
    public class LectureGroupService : ILectureGroupService
    {
        private readonly ILectureGroupRepository _lectureGroupRepository;
        private readonly ILectureGroupCourseRepository _lectureGroupCourseRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILectureAttendanceRepository _lectureAttendanceRepository;
        private readonly ILectureRepository _lectureRepository;

        public LectureGroupService(ILectureGroupRepository lectureGroupRepository,
            ILectureGroupCourseRepository lectureGroupCourseRepository, 
            IProfessorRepository professorRepository,
            ICourseRepository courseRepository,
            ILectureAttendanceRepository lectureAttendanceRepository,
            ILectureRepository lectureRepository) 
        {
            _lectureGroupCourseRepository = lectureGroupCourseRepository;
            _lectureGroupRepository = lectureGroupRepository;
            _professorRepository = professorRepository;
            _courseRepository = courseRepository;
            _lectureAttendanceRepository = lectureAttendanceRepository;
            _lectureRepository = lectureRepository;
        }
        public async Task<LectureGroup> Create(LectureGroupDTO data)
        {
            LectureGroup lectureGroup = new LectureGroup()
            {
                Id = Guid.NewGuid().ToString(),
                Name = data.Name,
                ProfessorId = data.ProfessorId,
            };

            LectureGroup createdLectureGroup = await _lectureGroupRepository.Create(lectureGroup);

            foreach(long course in data.SelectedCourses)
            {
                LectureGroupCourse lectureGroupCourse = new LectureGroupCourse()
                {
                    Id = Guid.NewGuid().ToString(),
                    CourseId = course,
                    LectureGroupId = createdLectureGroup.Id
                };

                LectureGroupCourse createdLectureGroupCourse = await _lectureGroupCourseRepository.Create(lectureGroupCourse);

                createdLectureGroup.Courses.Add(createdLectureGroupCourse);
            }

            return createdLectureGroup;
        }

        public async Task<LectureGroup> Delete(string lectureGroupId)
        {
            LectureGroup lectureGroup = await _lectureGroupRepository.GetById(lectureGroupId);

            foreach(LectureGroupCourse course in lectureGroup.Courses)
            {
                _lectureGroupCourseRepository.Delete(course);
            }

            return await _lectureGroupRepository.Delete(lectureGroup);
        }

        public async Task<LectureGroup> Get(string lectureGroupId)
        {
            return await _lectureGroupRepository.GetById(lectureGroupId);
        }

        public async Task<List<LectureGroup>> ListByProfessor(string professorId)
        {
            return await _lectureGroupRepository.ListByProfessorId(professorId);
        }

        public async Task<LectureGroup> Update(LectureGroupDTO data)
        {
            var professor = _professorRepository.GetById(data.ProfessorId);

            LectureGroup lectureGroup = await _lectureGroupRepository.GetById(data.Id);

            foreach (LectureGroupCourse course in lectureGroup.Courses)
            {
                _lectureGroupCourseRepository.Delete(course);
            }

            lectureGroup.Name = data.Name;

            LectureGroup updatedLectureGroup = await _lectureGroupRepository.Update(lectureGroup);

            foreach (long course in data.SelectedCourses)
            {
                LectureGroupCourse lectureGroupCourse = new LectureGroupCourse()
                {
                    Id = Guid.NewGuid().ToString(),
                    CourseId = course,
                    LectureGroupId = updatedLectureGroup.Id
                };

                LectureGroupCourse createdLectureGroupCourse = await _lectureGroupCourseRepository.Create(lectureGroupCourse);

                updatedLectureGroup.Courses.Add(createdLectureGroupCourse);
            }

            return updatedLectureGroup;
        }
        public async Task<LectureGroupDTO> PrepareForCreate(string professorId)
        {
            LectureGroupDTO data = new LectureGroupDTO()
            {
                ProfessorId = professorId,
                Courses = new List<Course>(),
                SelectedCourses = new List<long>()
            };
            
            List<CourseProfessor> coursesProfessor = await _courseRepository.GetCoursesForProfessor(professorId);
            List<CourseAssistant> coursesAssistant = await _courseRepository.GetCoursesForAssistant(professorId);

            data.Courses.AddRange(coursesProfessor.Select(c => c.Course).ToList());
            data.Courses.AddRange(coursesAssistant.Select(c => c.Course).ToList());

            data.Courses = data.Courses.GroupBy(d => d.Id).Select(g => g.First()).ToList();

            return data;
        }
        public async Task<LectureGroupDTO> PrepareForUpdate(string professorId, string lectureGroupId)
        {
            LectureGroup lectureGroup = await _lectureGroupRepository.GetById(lectureGroupId);

            if(lectureGroup != null)
            {
                LectureGroupDTO data = new LectureGroupDTO()
                {
                    Id = lectureGroup.Id,
                    ProfessorId = professorId,
                    Name = lectureGroup.Name,
                    Courses = new List<Course>(),
                    SelectedCourses = new List<long>()
                };

                List<CourseProfessor> coursesProfessor = await _courseRepository.GetCoursesForProfessor(professorId);
                List<CourseAssistant> coursesAssistant = await _courseRepository.GetCoursesForAssistant(professorId);

                data.Courses.AddRange(coursesProfessor.Select(c => c.Course).ToList());
                data.Courses.AddRange(coursesAssistant.Select(c => c.Course).ToList());

                data.Courses = data.Courses.GroupBy(d => d.Id).Select(g => g.First()).ToList();

                return data;
            }

            throw new InvalidOperationException();
        }

        public async Task<List<long?>> GetCoursesForLectureGroup(string lectureGroupId)
        { 
            List<LectureGroupCourse> tempLgcList = await _lectureGroupCourseRepository.ListByLectureGroupId(lectureGroupId);
            List<long?> courseIds = new List<long?>();
            foreach (LectureGroupCourse l in tempLgcList)
            {
                courseIds.Add(l.CourseId);
            }
            
            return courseIds;
        }

        public List<string> SelectLecturesForGroup(List<Lecture> lectures, List<long?> courseIds)
        {
            // Ensure courseIds is not empty to avoid unnecessary processing
            if (courseIds == null || !courseIds.Any())
            {
                return new List<string>(); // Return an empty list if courseIds is empty
            }

            // Group the lectures by LectureId and return a list of Lecture IDs
            var result = lectures
                .GroupBy(l => l.Id) // Group by LectureId
                .Where(g => g.SelectMany(l => l.Courses) // Flatten the courses in each group
                    .Where(c => courseIds.Contains(c.CourseId)) // Filter courses by the provided courseIds
                    .Select(c => c.CourseId) // Select only the CourseId
                    .Distinct() // Get distinct CourseIds
                    .Count() == courseIds.Count) // Compare the count with the original courseIds count
                .Select(g => g.Key) // Select the LectureId
                .ToList(); // Convert to a list

            return result;
        }

        public List<Lecture> GetLectures(List<string> lectureIds)
        {
            return _lectureRepository.GetLecturesByIds(lectureIds);
        }
    }
}
