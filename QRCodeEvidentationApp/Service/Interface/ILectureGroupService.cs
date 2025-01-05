using NuGet.Protocol.Plugins;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;

namespace QRCodeEvidentationApp.Service.Interface
{
    public interface ILectureGroupService
    {
        /// <summary>
        /// Creates a lecture group
        /// </summary>
        /// <param name="lectureGroup">Lecture group that will be created</param>
        /// <param name="courses">Courses that will be included in the lecture group</param>
        /// <returns>The created lecture group</returns>
        public LectureGroup Create(LectureGroupDTO data);
        /// <summary>
        /// Get details of a certain lecture group
        /// </summary>
        /// <param name="lectureGroupId">Id of the searched lecture group</param>
        /// <returns>The lecture group with the searched id</returns>
        public Task<LectureGroup> Get(string lectureGroupId);
        /// <summary>
        /// Get the lecture groups created by certain professor
        /// </summary>
        /// <param name="professorId">Id of the professor who created the searched lecture groups</param>
        /// <returns>Lecture groups created by the certain professor</returns>
        public Task<List<LectureGroup>> ListByProfessor (string professorId);
        /// <summary>
        /// Get the needed details for the create page.
        /// </summary>
        /// <param name="professorId">Professor which will create the lecture group</param>
        /// <returns>Needed details for that professor</returns>
        public Task<LectureGroupDTO> PrepareForCreate(string professorId);

        public void Delete(string id);
        
        public CourseGroupAnalyticsDTO GetLecturesCourseGroupAnalytics(List<Lecture> lectures, string id);
    }
}
