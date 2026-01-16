using API_studentManagement.Dto;
using API_studentManagement.Exceptions;
using API_studentManagement.Interface;
using API_studentManagement.Models;
using AutoMapper;

namespace API_studentManagement.Service
{
    public class CourseService:ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository repository, IMapper mapper, IEnrollmentRepository enrollmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses=await _repository.GetAllCoursesAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course=await _repository.GetCourseByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }
            return _mapper.Map<CourseDto>(course);
        }

        public async Task AddCourseAsync(CourseDto courseDto)
        {
            var addCourse=_mapper.Map<Course>(courseDto);
            await _repository.AddCourseAsync(addCourse);
        }

        public async Task UpdateCourseAsync(CourseDto courseDto)
        {
            var existingCourse = await _repository.GetCourseByIdAsync(courseDto.CourseId);

            if(existingCourse==null)
            {
                throw new NotFoundException("Course not found");
            }
            _mapper.Map(courseDto, existingCourse);
            await _repository.UpdateCourseAsync(existingCourse);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var deleteCourse = await _repository.GetCourseByIdAsync(id);
            var enrolledCourse = await _enrollmentRepository.IsCourseAlreadyEnrolledAsync(id);
            
            if (deleteCourse==null)
            {
                throw new NotFoundException("Course not found");
            }

            if(enrolledCourse)
            {
                throw new Exception("Cannot delete course because students are enrolled in this course");
            }

            await _repository.DeleteCourseAsync(id);
        }
    }
}
