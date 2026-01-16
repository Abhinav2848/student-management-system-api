using API_studentManagement.Enum;
using System.ComponentModel.DataAnnotations;

namespace API_studentManagement.Models
{
    public class User
    {
        [Key]
        public int UserId {  get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
