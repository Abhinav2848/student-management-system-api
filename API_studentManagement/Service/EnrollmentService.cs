using API_studentManagement.Dto;
using API_studentManagement.Exceptions;
using API_studentManagement.Interface;
using API_studentManagement.Models;


namespace API_studentManagement.Service
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentrepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEmailService _emailService;

        public EnrollmentService(IEnrollmentRepository enrollmentrepository, IStudentRepository studentRepository, ICourseRepository courseRepository, IEmailService emailService)
        {
            _enrollmentrepository = enrollmentrepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId)
        {
            var studdent = await _studentRepository.GetStudentByIdAsync(studentId);

            if (studdent == null)
            {
                throw new NotFoundException("Student not found");
            }

            var enrollments = await _enrollmentrepository.GetEnrollmentsByStudentIdAsync(studentId);
            return enrollments.Select(e => e.Course);
        }



        public async Task EnrollStudentAsync(EnrollmentDto enrollmentDto)
        {
            var student = await _studentRepository.GetStudentByIdAsync(enrollmentDto.StudentId);
            if (student == null)
                throw new NotFoundException("Student not found");

            var course = await _courseRepository.GetCourseByIdAsync(enrollmentDto.CourseId);
            if (course == null)
                throw new NotFoundException("Course not found");

            var alreadyEnrolled =
                await _enrollmentrepository.IsAlreadyEnrolledAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);

            if (alreadyEnrolled)
                throw new Exception("Student already enrolled in this course");

            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId
            };

            await _enrollmentrepository.AddEnrollmentAsync(enrollment);

            string subject = "Enrollment Confirmation";

            string body =
            $@"
            <div style='font-family:Arial, sans-serif; font-size:14px; color:#333; line-height:1.6;'>
                <p>Dear <b>{student.StudentName}</b>,</p>

                <p>We are pleased to confirm that you have been <b>successfully enrolled</b> in the following course:</p>

                <div style='padding:12px; border:1px solid #ddd; border-radius:6px; background:#f9f9f9; width:fit-content;'>
                    <p style='margin:0;'><b>Course Name:</b> {course.CourseTitle}</p>
                </div>

                <p style='margin-top:15px;'>If you have any questions, feel free to contact us.</p>

                <p>Thank you,<br>
                <b>Course Management Team</b></p>
            </div>";

            var mailRequest = new MailRequest
            {
                ToEmail = student.StudentEmail,
                Subject = subject,
                Body = body
            };

            await _emailService.SendEmailAsync(mailRequest);
        }

        public async Task UnEnrollStudentAsync(EnrollmentDto enrollmentDto)
        {
            var enrolled = await _enrollmentrepository.IsAlreadyEnrolledAsync(enrollmentDto.StudentId,enrollmentDto.CourseId);

            var student = await _studentRepository.GetStudentByIdAsync(enrollmentDto.StudentId);
            if (student == null)
                throw new NotFoundException("Student not found");

            if (!enrolled)
            {
                throw new Exception("Student not enrolled in this course");
            }
            var course = await _courseRepository.GetCourseByIdAsync(enrollmentDto.CourseId);
            if (course == null)
                throw new NotFoundException("Course not found");

            await _enrollmentrepository.UnEnrollStudentAsync(enrollmentDto.StudentId,enrollmentDto.CourseId);

            string subject = "Unenrollment Confirmation - Course Update";
            string body =
                    $@"
                    <div style='font-family:Segoe UI, sans-serif; font-size:14px; color:#2c2c2c; line-height:1.6;'>
                        <p>Hello {student.StudentName},</p>

                        <p>Your course registration has been updated successfully.</p>

                        <p><b>Status:</b> Unenrolled</p>
                        <p><b>Course:</b> {course.CourseTitle}</p>

                        <p>If you have any questions, please reach out to our support team.</p>

                        <p>Best regards,<br>
                        <b>Academic Support Team</b></p>
                    </div>";


            var mailRequest = new MailRequest
            {
                ToEmail = student.StudentEmail,
                Subject = subject,
                Body = body
            };

            await _emailService.SendEmailAsync(mailRequest);



        }
    }
}
