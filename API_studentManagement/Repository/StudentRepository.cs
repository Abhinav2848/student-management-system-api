using API_studentManagement.Exceptions;
using API_studentManagement.Interface;
using API_studentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace API_studentManagement.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var studentlist = await _context.Students.ToListAsync();
            return studentlist;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student=await _context.Students.FindAsync(id);
    
            if(student==null)
            {
                throw new NotFoundException("Student not found");
            }

            return student;
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
        
            if (student != null)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Student not found");
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            var deleteStudent=await _context.Students.FindAsync(id);

            if(deleteStudent != null)
            {
                _context.Students.Remove(deleteStudent);
               await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Student not found");
            }
        }
    }
}
