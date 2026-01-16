using API_studentManagement.Dto;
using API_studentManagement.Exceptions;
using API_studentManagement.Interface;
using API_studentManagement.Models;
using AutoMapper;

namespace API_studentManagement.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repository,IEnrollmentRepository repositorys,IMapper mapper)
        {
            _repository = repository;
            _enrollmentRepository = repositorys;
            _mapper = mapper;
            
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var studentlist= await _repository.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(studentlist);
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student= await _repository.GetStudentByIdAsync(id);

            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            return _mapper.Map<StudentDto>(student);
        }

        public async Task AddStudentAsync(StudentDto studentDto)
        {
            var addStudent=_mapper.Map<Student>(studentDto);
            await  _repository.AddStudentAsync(addStudent);
        }

        public async Task UpdateStudentAsync(StudentDto studentDto)
        {
            var existingStudent = await _repository.GetStudentByIdAsync(studentDto.StudentId);

            if(existingStudent==null)
            {
                throw new NotFoundException("Student not found");
            }

           _mapper.Map(studentDto,existingStudent);
            await _repository.UpdateStudentAsync(existingStudent);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var deleteStudent = await _repository.GetStudentByIdAsync(id);
            var enrolledStudent = await _enrollmentRepository.IsStudentAlreadyEnrolledAsync(id);

            if (deleteStudent == null)
            {
                throw new NotFoundException("Student not found");
            }

            if(enrolledStudent)
            {
                throw new Exception("Cannot delete student because student is enrolled in course");
            }

            await _repository.DeleteStudentAsync(id);

        }
    }
}
