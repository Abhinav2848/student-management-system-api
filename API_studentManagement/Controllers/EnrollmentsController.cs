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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet("GetStudentCourse/{studentId}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetStudentCourse(int studentId)
        {
            try
            {
                var courses = await _service.GetStudentCoursesAsync(studentId);
                return Ok(courses);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("EnrollStudent")]
        [Authorize(Roles = "Admin,Staff,Student")]
        public async Task<IActionResult> EnrollStudent(EnrollmentDto enrollmentDto)
        {


            try
            {
                await _service.EnrollStudentAsync(enrollmentDto);
                return Ok("Student enrolled successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UnEnrollStudent")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UnEnrollStudent(EnrollmentDto enrollmentDto)
        {
            try
            {
                await _service.UnEnrollStudentAsync(enrollmentDto);
                return Ok("Student unenrolled successfull");
            }
            catch (NotFoundException ex)
            {
                {
                    return NotFound(ex.Message);
                }

            }
        }
    }
}
