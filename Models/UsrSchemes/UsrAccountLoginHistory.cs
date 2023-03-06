using System;
using System.Collections.Generic;

namespace SentryBex.Models.UsrSchemes
{
    public partial class UsrAccountLoginHistory
    {
        public long Id { get; set; }
        public long AccountFk { get; set; }
        public DateTime Created { get; set; }
    }
}
