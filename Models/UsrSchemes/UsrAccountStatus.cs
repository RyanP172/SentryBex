using System;
using System.Collections.Generic;

namespace SentryBex.Models.UsrSchemes
{
    public partial class UsrAccountStatus
    {
        public byte Id { get; set; }
        public string Status { get; set; } = null!;
        public string StatusDesc { get; set; } = null!;
    }
}
