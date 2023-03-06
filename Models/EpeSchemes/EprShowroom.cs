using System;
using System.Collections.Generic;

namespace SentryBex.Models.EpeSchemes
{
    public partial class EprShowroom : Showroom
    {
        public int Id { get; set; }
        public int CompanyFk { get; set; }
        public string Name { get; set; } = null!;
        public string ShopCode { get; set; } = null!;
        public int OrderPrefix { get; set; }
        public int DefaultConsultantFk { get; set; }
        public int? MonthlyBudget { get; set; }
        public string? State { get; set; }
        public short? LoadDay { get; set; }
    }
}
