using API_studentManagement.Dto;
using API_studentManagement.Models;

namespace API_studentManagement.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDto courseDto);
        Task UpdateCourseAsync(CourseDto courseDto);
        Task DeleteCourseAsync(int id);
    }
}
