using QRCodeEvidentationApp.Models;

namespace QRCodeEvidentationApp.Repository.Interface
{
    public interface ILectureGroupCourseRepository
    {
        /// <summary>
        /// Creates the Lecture Group Course from the parameter
        /// </summary>
        /// <param name="lectureGroupCourse">Lecture group course to be created</param>
        /// <returns>The created lecture group course</returns>
        public Task<LectureGroupCourse> Create(LectureGroupCourse lectureGroupCourse);
        /// <summary>
        /// Deletes the Lecture Group Course from the parameter
        /// </summary>
        /// <param name="lectureGroupCourse">Lecture group course to be deleted</param>
        /// <returns>The deleted lecture group course</returns>
        public Task<LectureGroupCourse> Delete(LectureGroupCourse lectureGroupCourse);
        /// <summary>
        /// Returns all the Lecture Groups Courses in the the LectureGroupId from the parameter
        /// </summary>
        /// <param name="lectureGroupId">Lecture group courses that are in the LectureGroupId</param>
        /// <returns>Lecture group courses that are in the LectureGroupId</returns>
        public Task<List<LectureGroupCourse>> ListByLectureGroupId(string lectureGroupId);

    }
}
