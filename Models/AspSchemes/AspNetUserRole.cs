using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SentryBex.Models.AspSchemes
{
    [NotMapped]
    public partial class AspNetUserRole
    {

        public string UserId { get; set; } = null!;

        public string RoleId { get; set; } = null!;
    }
}
