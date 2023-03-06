using System;
using System.Collections.Generic;

namespace SentryBex.Models.UsrSchemes
{
    public partial class UsrGroup
    {
        public short Id { get; set; }
        public string GroupName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
