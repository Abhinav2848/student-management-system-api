using API_studentManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_studentManagement.Dto
{
    public class EnrollmentDto
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage ="StudentId is required")]
        public int StudentId { get; set; }


        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }


    }
}
