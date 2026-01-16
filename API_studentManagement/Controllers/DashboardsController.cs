using API_studentManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_studentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Dashboard")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ViewDashboard()
        {
            var totalStudents=await _context.Students.CountAsync();
            var totalCourses =await _context.Courses.CountAsync();
            var totalEnrollments =await _context.Enrollments.CountAsync();
            var totalUsers = await _context.Users.CountAsync();

            return Ok(new
            {
                TotalStudents = totalStudents,
                TotalCourses = totalCourses,
                TotalEnrollments = totalEnrollments,
                TotalUsers = totalUsers

            });
        }
    }
}
