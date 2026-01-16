using API_studentManagement.Interface;
using API_studentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace API_studentManagement.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Register(User user)
        {
            var users = await _context.Users.FirstOrDefaultAsync(i=>i.Email==user.Email);

            if (users!=null)
                return false;

            

            _context.Users.Add(user);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> Login(User user)
        {
            var existingUser=await _context.Users.FirstOrDefaultAsync(u=>u.Email==user.Email);

            if (existingUser == null)
            {
                return null;
            }
            return existingUser;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var users = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return users;
        }
    }
}
