using System;
using System.Collections.Generic;

namespace SentryBex.Models.EpeSchemes
{
    public partial class EpeEmployeeGroupLink
    {
        public long Id { get; set; }
        public long EmployeeFk { get; set; }
        public short GroupFk { get; set; }
        public DateTime Created { get; set; }
    }
}
