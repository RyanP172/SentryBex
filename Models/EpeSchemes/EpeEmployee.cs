using System;
using System.Collections.Generic;

namespace SentryBex.Models.EpeSchemes
{
    public partial class EpeEmployee : Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public long AccountFk { get; set; }
        public string? Code { get; set; }
        public bool IsContractor { get; set; }
        public string? ContractorTypeFk { get; set; }
        public int? DefaultShowroomFk { get; set; }
        public int? MaxLeadCount { get; set; }
        public int? MonthlyBudget { get; set; }
    }
}
