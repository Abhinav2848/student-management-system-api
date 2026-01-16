using API_studentManagement.Dto;
using API_studentManagement.Models;

namespace API_studentManagement.Interface
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId);
        Task EnrollStudentAsync(EnrollmentDto enrollmentDto);
        Task UnEnrollStudentAsync(EnrollmentDto enrollmentDto);
    }
}
