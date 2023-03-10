using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SentryBex.Models.UsrSchemes
{
    public partial class UsrAccount : UserAccount
    {
        public long Id { get; set; }
        /// <summary>
        /// must be a valid email address
        /// </summary>
        public string UserName { get; set; } = null!;
        public string SamAccountName { get; set; } = null!;
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }

        
              
        public DateTime Created { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid? ResetPwdGuid { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ResetPwdDatetime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Modified { get; set; }
        public string? Status { get; set; }
    }
}
