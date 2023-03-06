using System.ComponentModel.DataAnnotations;

namespace SentryBex.Dtos
{
    public class LogInDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
