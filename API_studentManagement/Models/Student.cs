using System.ComponentModel.DataAnnotations;

namespace API_studentManagement.Models
{
    public class Student
    {
        [Key]
        public int StudentId {  get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhone { get; set; }
        public DateOnly DateOfBirth {  get; set; }
        public string StudentAddress {  get; set; }
        public DateTime CreatedAt {  get; set; }=DateTime.Now;

    }
}
