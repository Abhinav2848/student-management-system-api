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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet("GetAllStudents")]
        [Authorize(Roles = "Admin,Staff,Student")]

        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _service.GetAllStudentsAsync();
            return Ok(students);
        }

      
        [HttpGet("GetStudentBySearch")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAllStudentsBySearch(int page = 1, int pageSize = 5, string? search = null)
        {
            
                var students = await _service.GetAllStudentsAsync();

                if (search != null)
                    students = students.Where(s => s.StudentName.Contains(search)).ToList();

                var result = students.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                return Ok(result);
            
            
        }

        //public async Task<IActionResult> GetStudentById(int id)
        //{
        //    try
        //    {
        //        var student = await _service.GetStudentByIdAsync(id);
        //        return Ok(student);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //}

        [HttpPost("AddStudent")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> AddStudent(StudentDto studentDto)
        {
            await _service.AddStudentAsync(studentDto);
            return Ok("Student added successfully");
        }

        [HttpPut("UpdateStudent")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UpdateStudent(StudentDto studentDto)
        {
            try
            {

                await _service.UpdateStudentAsync(studentDto);
                return Ok("Student updated successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteStudent{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _service.DeleteStudentAsync(id);
                return Ok("Student deleted successfully");
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
