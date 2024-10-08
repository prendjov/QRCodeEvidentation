using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Interface;

namespace QRCodeEvidentationApp.Service.Implementation;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IStudentCourseRepository _studentCourseRepository;
    
    public StudentService(IStudentRepository studentRepository, IStudentCourseRepository studentCourseRepository)
    {
        _studentRepository = studentRepository;
        _studentCourseRepository = studentCourseRepository;
    }
    
    public Task<Student> GetStudentFromUserEmail(string email)
    {
        return _studentRepository.GetStudentByEmail(email);
    }

    public bool CheckStudentInCourse(string studentIndex, List<long?> courseId)
    {
        List<long?> coursesForStudent = _studentRepository.GetCoursesForStudent(studentIndex);
        foreach (long? id in courseId)
        {
            foreach (long? id_student in coursesForStudent)
            {
                if (id_student == id)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public List<StudentCourse> GetStudentsForProfessor(string professorId)
    {
        return _studentCourseRepository.GetStudentsForProfessor(professorId);
    }
}