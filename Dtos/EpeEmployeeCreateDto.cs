using System.ComponentModel.DataAnnotations;

namespace SentryBex.Dtos
{
    public class EpeEmployeeCreateDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "First name is a string with a maximum length of 80")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,80}$", ErrorMessage = "First name can only contain common characters")]
        public string FirstName { get; set; } = null!;
        [StringLength(20, ErrorMessage = "First name is a string with a maximum length of 80")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,80}$", ErrorMessage = "Middle name can only contain common characters")]
        public string? MiddleName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "First name is a string with a maximum length of 80")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,80}$", ErrorMessage = "Last name can only contain common characters")]
        public string LastName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        [StringLength(10, ErrorMessage = "Sam account name is a string with a maximum length of 10")]
        public string? Code { get; set; }
        public bool IsContractor { get; set; }
        public string? ContractorTypeFk { get; set; }
        public int DefaultShowroomFk { get; set; }
        public int? MaxLeadCount { get; set; }
        public int? MonthlyBudget { get; set; }
        public int CompanyId { get; set; }

        [StringLength(80, ErrorMessage = "Email is a string with a maximum length of 80")]
        public string Email { get; set; } = null!;
        [StringLength(40, ErrorMessage = "Sam account name is a string with a maximum length of 40")]
        public string SamAccountName { get; set; } = null!;
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }
        //public DateTime Created { get; set; }
        //public Guid? ResetPwdGuid { get; set; }
        //public DateTime? ResetPwdDatetime { get; set; }
        //public DateTime? Modified { get; set; }
        public string? Status { get; set; }

        public string? DefaultRole { get; set; }
    }
}
