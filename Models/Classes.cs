using Microsoft.EntityFrameworkCore;
using SentryBex.Models.AspSchemes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace SentryBex.Models
{
    public class Classes
    {
    }
    [NotMapped]
    public class Employee
    {
        public Employee()
        {
            this.showRooms = new List<Showroom>();
            this.permissions = new List<Permission>();
            this.account = new UserAccount();
            this.netUser = new NetUser();
        }
        public NetUser netUser { get; set; }
        public UserAccount account { get; set; }
        public List<Showroom> showRooms { get; set; }
        public List<Permission> permissions { get; set; }
    }

    [NotMapped]
    public class Showroom
    {
        public Showroom()
        {

        }

    }
    [NotMapped]

    public class Permission
    {
        public Permission()
        {

        }
    }

    [NotMapped]

    public class UserAccount
    {
        public UserAccount()
        {


        }
    }

    /*[NotMapped]
    public class AspNetUserRole
    {
        public AspNetUserRole()
        {

        }
        public string RoleId { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }*/

    [NotMapped]
    public class NetUser : AspNetUser
    {
        public NetUser()
        {

        }
    }
}
