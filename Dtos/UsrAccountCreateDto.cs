using System.Security.Principal;

namespace SentryBex.Dtos
{
    public class UsrAccountCreateDto
    {
        public string Email { get; set; } = null!;
        public string SamAccountName { get; set; } = null!;
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }
        //public DateTime Created { get; set; }
        //public Guid? ResetPwdGuid { get; set; }
        //public DateTime? ResetPwdDatetime { get; set; }
        //public DateTime? Modified { get; set; }
        public string? Status { get; set; }



    }
}


