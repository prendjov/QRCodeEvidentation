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
        public Task<LectureGroup> Create(LectureGroupDTO data);
        /// <summary>
        /// Updates a certain lecture group
        /// </summary>
        /// <param name="lectureGroupId">Id of the lecture group that will be edited</param>
        /// <param name="name">Edited name of the lecture group</param>
        /// <param name="courses">Updated list of courses included the lecture group</param>
        /// <returns>The updated lecture group</returns>
        public Task<LectureGroup> Update(LectureGroupDTO data);
        /// <summary>
        /// Removes a certain lecture group
        /// </summary>
        /// <param name="lectureGroupId">Id of the lecture group that will be removed</param>
        /// <returns>The removed lecture group</returns>
        public Task<LectureGroup> Delete(string lectureGroupId);
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
        /// Get the needed details for the update page
        /// </summary>
        /// <param name="professorId">Professor which will update the lecture group</param>
        /// <param name="lectureGroupId">The lecture group which will be updated</param>
        /// <returns>Needed details for that professor and lecture group</returns>
        public Task<LectureGroupDTO> PrepareForUpdate(string professorId, string lectureGroupId);
        
        /// <summary>
        /// Get all the courses for a certain Lecture Group.
        /// </summary>
        /// <param name="lectureGroupId">The id of the lecture group.</param>
        /// <returns>A list of courses associated with the specified lecture group.</returns>
        public Task<List<long?>> GetCoursesForLectureGroup(string lectureGroupId);

        /// <summary>
        /// Selects all the lectures that are connected with certain lecture group.
        /// It groups the lectures based on ID and if a certain Lecture points to all the ids
        /// that are in courseIds, it means that lecture is of that group and it should be added
        /// to the result.
        /// </summary>
        /// <param name="lectures">The lectures for a certain professor.</param>
        /// <param name="courseIds">The courses that are in the lecture group.</param>
        /// <returns>A list of lecture ids that belong to certain lecture group.</returns>
        public List<string> SelectLecturesForGroup(List<Lecture> lectures, List<long?> courseIds);
        
        public List<Lecture> GetLectures(List<string> lectureIds);
    }
}
