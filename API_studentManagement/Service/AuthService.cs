using API_studentManagement.Dto;
using API_studentManagement.Interface;
using API_studentManagement.Models;
using AutoMapper;

namespace API_studentManagement.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<bool> Register(UserDto userDto)
        {
            var existingUser = await _repository.GetUserByEmail(userDto.Email);

            if (existingUser != null)
                return false;

            var newUser = _mapper.Map<User>(userDto);
  
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            return await _repository.Register(newUser);
        }


        public async Task<UserDto> Login(LoginDto loginDto)
        {

            var user = await _repository.GetUserByEmail(loginDto.Email);

         
            if(user==null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
