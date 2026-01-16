using API_studentManagement.Dto;
using API_studentManagement.Exceptions;
using API_studentManagement.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_studentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet("GetAllCourses")]
        [Authorize(Roles="Admin,Staff,Student")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses=await _service.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("GetCourseBySearch")]
        [Authorize(Roles = "Admin,Staff,Student")]

        public async Task<IActionResult> GetAllCoursesBySearch(int page = 1, int pageSize = 5, string? search = null)
        {

            var courses = await _service.GetAllCoursesAsync();

            if (search != null)
                courses = courses.Where(s => s.CourseTitle.Contains(search)).ToList();

            var result = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(result);


        }
        //public async Task<IActionResult> GetCoursesById(int id)
        //{
        //    try
        //    {
        //        var course = await _service.GetCourseByIdAsync(id);
        //        return Ok(course);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpPost("AddCourse")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> AddCourse(CourseDto courseDto)
        {
            await _service.AddCourseAsync(courseDto);
            return Ok("Course added successfull");
        }

        [HttpPut("UpdateCourse")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UpdateCourse(CourseDto courseDto)
        {
            try
            {
                await _service.UpdateCourseAsync(courseDto);
                return Ok("Course updated successfull");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteCourse{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _service.DeleteCourseAsync(id);
                return Ok("Course deleted successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
