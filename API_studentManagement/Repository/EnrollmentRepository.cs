using API_studentManagement.Interface;
using API_studentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace API_studentManagement.Repository
{
    public class EnrollmentRepository:IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            var enrollement = await _context.Enrollments.Include(c=>c.Course).Where(s=>s.StudentId==studentId).ToListAsync();
            return enrollement;
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UnEnrollStudentAsync(int studentId,int courseId)
        {
        
            var enrolled=await _context.Enrollments.FirstOrDefaultAsync(i=>i.StudentId==studentId &&i.CourseId==courseId);

            if(enrolled==null)
            {
                throw new Exception("Student not enrolled in this course");
            }

             _context.Enrollments.Remove(enrolled);
             await _context.SaveChangesAsync();   
        }

        public async Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId)
        {
            var Enrolled=await _context.Enrollments.AnyAsync(e=>e.StudentId==studentId && e.CourseId==courseId);
            return Enrolled;
        }

        public async Task<bool> IsStudentAlreadyEnrolledAsync(int studentId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.StudentId == studentId);
        }


        public async Task<bool> IsCourseAlreadyEnrolledAsync(int courseId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.CourseId == courseId);
        }
    }
}
