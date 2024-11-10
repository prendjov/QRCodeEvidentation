using System.Linq.Expressions;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Models.DTO;
using QRCodeEvidentationApp.Models.DTO.AnalyticsDTO;
using QRCodeEvidentationApp.Repository.Implementation;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILectureCoursesRepository _lectureCoursesRepository;
    private readonly ILectureAttendanceRepository _lectureAttendanceRepository;

    public CourseService(ICourseRepository courseRepository, ILectureCoursesRepository lectureCoursesRepository,
        ILectureAttendanceRepository lectureAttendanceRepository)
    {
        _courseRepository = courseRepository;
        _lectureCoursesRepository = lectureCoursesRepository;
        _lectureAttendanceRepository = lectureAttendanceRepository;
    }

    public async Task<List<CourseProfessor>> GetCoursesForProfessor(string? professorId)
    {
        return await _courseRepository.GetCoursesForProfessor(professorId);
    }

    public async Task<List<CourseAssistant>> GetCoursesForAssistant(string? assistantId)
    {
        return await _courseRepository.GetCoursesForAssistant(assistantId);
    }

    public List<long?> GetCoursesIdByLectureId(string? lectureId)
    {
        try
        {
            return _lectureCoursesRepository.GetCoursesForLecture(lectureId);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException();
        }
    }

    public List<Course> GetCourses(string? teacherId)
    {
        return _courseRepository.GetCourses(teacherId).Result;
    }

    public List<string?> GetLectureForCourseId(long? courseId)
    {
        List<string?> lectureIds = _courseRepository.GetLectureForCourseId(courseId).Result;
        return lectureIds;
    }
    
    public CourseAnalyticsDTO GetCourseStatistics(List<Lecture> lecturesForProfessor, List<StudentCourse> studentCourses, List<string?> lecturesForCourse)
    {
        if (lecturesForProfessor.Count == 0)
        {
            return new CourseAnalyticsDTO()
            {
                EachLectureAnalytics = new List<CourseAnalyticsDTO>()
            };
        }

        List<LectureAttendance> lectureAttendances = _lectureAttendanceRepository.GetLectureAttendances(lecturesForCourse);
        
        CourseAnalyticsDTO courseAnalyticsDto = new CourseAnalyticsDTO();
        courseAnalyticsDto.EachLectureAnalytics = new List<CourseAnalyticsDTO>();
        List<string?> studentsAtProfessor = new List<string?>();
        foreach (StudentCourse studentCourse in studentCourses)
        {
            studentsAtProfessor.Add(studentCourse.StudentStudentIndex);
        }

        Dictionary<string, Lecture> lectureDictionary = lecturesForProfessor.ToDictionary(l => l.Id);
            
        // Group lecture attendances by LectureId
        var groupedAttendances = lectureAttendances
            .GroupBy(a => a.LectureId) // Group by LectureId
            .ToDictionary(g => g.Key, g => g.ToList());
        
        // Ensure all lectureIds are present in the dictionary with an empty list if not grouped
        foreach (var lectureId in lecturesForCourse)
        {
            if (!groupedAttendances.ContainsKey(lectureId))
            {
                groupedAttendances[lectureId] = new List<LectureAttendance>();
            }
        }
        
        foreach (var group in groupedAttendances)
        {
            string lectureId = group.Key;
            List<LectureAttendance> attendances = group.Value; // This is the list of LectureAttendance records for that LectureId
            int AtProfessor = 0;
            int notAtProfessor = 0;
            foreach (var attendance in attendances)
            {
                if (studentsAtProfessor.Contains(attendance.StudentIndex))
                {
                    AtProfessor += 1;
                    continue;
                }
                notAtProfessor += 1;
            }

            CourseAnalyticsDTO analyticsDtoForLecture = new CourseAnalyticsDTO();
            analyticsDtoForLecture.NumberOfAttendees = attendances.Count();
            analyticsDtoForLecture.AtProfessorNumberOfAttendees = AtProfessor;
            analyticsDtoForLecture.NotAtProfessorNumberOfAttendees = notAtProfessor;
            analyticsDtoForLecture.lectureId = lectureId;
            analyticsDtoForLecture.lecture = lectureDictionary[lectureId];
            courseAnalyticsDto.EachLectureAnalytics.Add(analyticsDtoForLecture);
        }

        foreach (var analytic in courseAnalyticsDto.EachLectureAnalytics)
        {
            courseAnalyticsDto.NumberOfAttendees += analytic.NumberOfAttendees;
            courseAnalyticsDto.AtProfessorNumberOfAttendees += analytic.AtProfessorNumberOfAttendees;
            courseAnalyticsDto.NotAtProfessorNumberOfAttendees += analytic.NotAtProfessorNumberOfAttendees;
        }

        return courseAnalyticsDto;
    }
}