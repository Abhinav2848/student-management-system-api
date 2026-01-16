using API_studentManagement.Dto;

namespace API_studentManagement.Interface
{
    public interface IAuthService
    {
        Task<bool> Register(UserDto userDto);
        Task<UserDto> Login(LoginDto loginDto);
    }
}
