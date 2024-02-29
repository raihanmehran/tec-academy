using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        public string Country { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}