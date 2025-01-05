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
        private readonly ICourseRepository _courseRepository;
        private readonly ILectureRepository _lectureRepository;

        public LectureGroupService(ILectureGroupRepository lectureGroupRepository,
            ICourseRepository courseRepository,
            ILectureRepository lectureRepository) 
        {
            _lectureGroupRepository = lectureGroupRepository;
            _courseRepository = courseRepository;
            _lectureRepository = lectureRepository;
        }
        public LectureGroup Create(LectureGroupDTO data)
        {
            LectureGroup createdLectureGroup = _lectureGroupRepository.Create(data);

            return createdLectureGroup;
        }

        public async Task<LectureGroup> Get(string lectureGroupId)
        {
            return await _lectureGroupRepository.GetById(lectureGroupId);
        }

        public async Task<List<LectureGroup>> ListByProfessor(string professorId)
        {
            return await _lectureGroupRepository.ListByProfessorId(professorId);
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

        public void Delete(string id)
        {
            LectureGroup lectureGroup = Get(id).Result;
            
            _lectureGroupRepository.Delete(lectureGroup);
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

        public List<Lecture> GetLectures(List<string> lectureIds)
        {
            return _lectureRepository.GetLecturesByIds(lectureIds);
        }
    }
}
