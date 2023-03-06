using System;
using System.Collections.Generic;

namespace SentryBex.Models.UsrSchemes
{
    public partial class UsrActivityLog
    {
        public string LogId { get; set; } = null!;
        public string? UserUuid { get; set; }
        public DateTime? LoggedDate { get; set; }
        public string? LogActivityType { get; set; }
        public string? LogDetail { get; set; }
        public string? LogActivityStatus { get; set; }
    }
}
