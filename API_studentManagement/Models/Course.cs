using System.ComponentModel.DataAnnotations;

namespace API_studentManagement.Models
{
    public class Course
    {
        [Key]
        public int  CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public int CourseDuration { get; set; }
        public decimal CourseFess {  get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

    }
}
