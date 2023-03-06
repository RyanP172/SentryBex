namespace SentryBex.Dtos
{
    public class EpeEmployeeCreateDto
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime? Dob { get; set; } 
        public string? Code { get; set; }
        public bool IsContractor { get; set; }
        public string? ContractorTypeFk { get; set; }
        public int DefaultShowroomFk { get; set; }
        public int? MaxLeadCount { get; set; }
        public int? MonthlyBudget { get; set; }
        public int CompanyId { get; set; }
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
