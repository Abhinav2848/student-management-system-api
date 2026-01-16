using API_studentManagement.Models;

namespace API_studentManagement.Interface
{
    public interface IAuthRepository
    {
        Task<bool> Register(User user);
        Task<User> Login(User user);
        Task<User> GetUserByEmail(string email);
    }
}
