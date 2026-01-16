using System.ComponentModel.DataAnnotations.Schema;

namespace API_studentManagement.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("course")]
        public int CourseId { get; set; }

        public DateTime EnrolledOn { get; set; } = DateTime.Now;
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
