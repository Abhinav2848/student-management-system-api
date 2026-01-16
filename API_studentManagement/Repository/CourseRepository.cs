using API_studentManagement.Exceptions;
using API_studentManagement.Interface;
using API_studentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace API_studentManagement.Repository
{
    public class CourseRepository:ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course=await _context.Courses.FindAsync(id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }
            return course;
        }

        public async Task AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            if(course != null)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Course not found");
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            var deleteCourse = await _context.Courses.FindAsync(id);

            if (deleteCourse != null)
            {
                _context.Courses.Remove(deleteCourse);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Course not found");
            }
        }
    }
}
