using System.ComponentModel.DataAnnotations;

namespace API_studentManagement.Dto
{
    public class StudentDto
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string StudentAddress { get; set; }
    }
}
