using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SentryBex.Models.EpeSchemes
{
    public partial class EpeEmployeeShowroomLink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int ShowroomFk { get; set; }
        public long EmployeeFk { get; set; }
    }
}
