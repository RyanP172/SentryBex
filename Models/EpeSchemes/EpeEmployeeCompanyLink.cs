using System;
using System.Collections.Generic;

namespace SentryBex.Models.EpeSchemes
{
    public partial class EpeEmployeeCompanyLink
    {
        public long Id { get; set; }
        public int CompanyFk { get; set; }
        public long EmployeeFk { get; set; }
    }
}
