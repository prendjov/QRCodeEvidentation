using Microsoft.AspNetCore.Authorization;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
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

        public LectureGroupService(ILectureGroupRepository lectureGroupRepository, ILectureGroupCourseRepository lectureGroupCourseRepository, IProfessorRepository professorRepository, ICourseRepository courseRepository) 
        {
            _lectureGroupCourseRepository = lectureGroupCourseRepository;
            _lectureGroupRepository = lectureGroupRepository;
            _professorRepository = professorRepository;
            _courseRepository = courseRepository;
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

    }
}
