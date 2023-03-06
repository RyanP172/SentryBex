using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SentryBex.Models.AspSchemes
{
    public partial class AspNetRole : Permission
    {
        public AspNetRole()
        {
            Users = new HashSet<AspNetUser>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        /*[JsonIgnore]*/
        /*[IgnoreDataMember]*/
        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
