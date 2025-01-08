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
        /// <param name="data">DTO object containing the input data from the form for creating
        /// Lecture Group</param>
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
        
        /// <summary>
        /// Deletes a lecture group. The delete behaviour is CASCADE, meaning if a Lecture Group is deleted, all
        /// the Lectures that have relationship with that LectureGroup are also deleted.
        /// </summary>
        /// <param name="id">The id of the LectureGroup.</param>
        public void Delete(string id);
        
        /// <summary>
        /// Returns a DTO object that contains the course group id, and Dictionary with key as Lecture and value
        /// as long. It is a DTO object that displays how many students has attended at each Lecture of the specified
        /// LectureGroup.
        /// </summary>
        /// <param name="lectures">List of Lectures.</param>
        /// <param name="id">The id of the lecture group.</param>
        public CourseGroupAnalyticsDTO GetLecturesCourseGroupAnalytics(List<Lecture> lectures, string id);
    }
}
