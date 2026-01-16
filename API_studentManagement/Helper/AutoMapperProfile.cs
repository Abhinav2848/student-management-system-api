using API_studentManagement.Dto;
using API_studentManagement.Models;
using AutoMapper;

namespace API_studentManagement.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student,StudentDto>().ReverseMap();
            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Enrollment,EnrollmentDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}
