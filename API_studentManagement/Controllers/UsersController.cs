using API_studentManagement.Dto;
using API_studentManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_studentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);

            if (user != null)
                return BadRequest("User already exists");

            var newUser = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = userDto.Role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

   
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signIn
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenValue,
                User = new
                {
                    user.UserId,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Role
                }
            });
        }

        [HttpGet]
        [Route("GetUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            var user = _context.Users.ToList();
            return Ok(user);

        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {

            //var email = User.FindFirstValue(ClaimTypes.Email);
            var email = User.FindFirst("Email")?.Value;

            var user= await _context.Users.FirstOrDefaultAsync(e=>e.Email==email);
           
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return Ok(new
            {
                user.UserId,
                user.FirstName, 
                user.LastName,
                user.Email,
                user.Role
            });
                 
        }

    }
}
