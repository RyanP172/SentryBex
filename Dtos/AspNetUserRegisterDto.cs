using System.ComponentModel.DataAnnotations;

namespace SentryBex.Dtos
{
    public class AspNetUserRegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Your confirm password did not match with your wanted password")]
        public string ConfirmPassword { get; set; }
    }
}
