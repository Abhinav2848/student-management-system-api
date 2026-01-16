using API_studentManagement.Models;

namespace API_studentManagement.Interface
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task AddEnrollmentAsync(Enrollment enrollment);
        Task UnEnrollStudentAsync(int studentId, int courseId);
        Task<bool> IsAlreadyEnrolledAsync(int studentId, int CourseId);
        Task<bool> IsStudentAlreadyEnrolledAsync(int studentId);
        Task<bool> IsCourseAlreadyEnrolledAsync(int courseId);
    }
}
