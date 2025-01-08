using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;

namespace QRCodeEvidentationApp.Repository.Interface
{
    public interface ILectureGroupRepository
    {
        /// <summary>
        /// Creates the Lecture Group from the parameter
        /// </summary>
        /// <param name="lectureGroup">Lecture group to be created</param>
        /// <returns>The created lecture group</returns>
        public LectureGroup Create(LectureGroupDTO lectureGroup);
        /// <summary>
        /// Updates the Lecture Group from the parameter
        /// </summary>
        /// <param name="lectureGroup">Lecture group to be updated</param>
        /// <returns>The updated lecture group</returns>
        public Task<LectureGroup> Update(LectureGroup lectureGroup);
        /// <summary>
        /// Deletes the Lecture Group from the parameter
        /// </summary>
        /// <param name="lectureGroup">Lecture group to be deleted</param>
        /// <returns>The deleted lecture group</returns>
        public LectureGroup Delete(LectureGroup lectureGroup);
        /// <summary>
        /// Returns the Lecture Group assigned to the ID
        /// </summary>
        /// <param name="lectureGroupId">ID from the lecture group that is searched</param>
        /// <returns>The searched lecture group</returns>
        public Task<LectureGroup> GetById(string lectureGroupId);
        /// <summary>
        /// Returns all the Lecture Groups created by the ProfessorId in the parameter
        /// </summary>
        /// <param name="professorId">ProfessorId that created the searched lecture groups</param>
        /// <returns>Lecture groups created by certain ProfessorId</returns>
        public Task<List<LectureGroup>> ListByProfessorId(string professorId);
    }
}
