using System;
using System.Collections.Generic;

namespace SentryBex.Models.EpeSchemes
{
    public partial class EpeEmployeeContractorCompanyLink
    {
        public int Id { get; set; }
        public int EmployeeFk { get; set; }
        public int ContractorCompanyFk { get; set; }
    }
}
