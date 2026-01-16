using API_studentManagement.Dto;
using API_studentManagement.Models;

namespace API_studentManagement.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);

        Task AddStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(StudentDto studentDto);
        Task DeleteStudentAsync(int id);
    }
}
