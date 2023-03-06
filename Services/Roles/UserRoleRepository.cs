using Microsoft.EntityFrameworkCore;
using SentryBex.Database;
using SentryBex.Models.AspSchemes;
using SentryBex.Models.EpeSchemes;
using SentryBex.Models.UsrSchemes;
using System.Collections;
using System.Xml.Linq;

namespace SentryBex.Services
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly AspNetContext _aspNetContext;
        public UserRoleRepository(AppDbContext context, AspNetContext aspNetContext)
        {
            _context = context;
            _aspNetContext = aspNetContext;
        }

        public async Task<IEnumerable> GetAspNetUsersInfoAsync()
        {
            var result = _aspNetContext.AspNetUsers.Select(u => new
            {
                id = u.Id,
                email = u.Email,
                epeId = (_aspNetContext.EpeEmployees
                                    .Where
                                    (e => e.AccountFk == (
                                    _aspNetContext.UsrAccounts.Where(a => a.UserName == u.Email)
                                                    .Select(a => a.Id)).FirstOrDefault()
                                                )).Select(e => e.Id).FirstOrDefault()
            });
            if (result.Count() > 0)
            {
                return await result.ToListAsync();
            }
            return null;

        }

        public async Task<AspNetRole> CreateNewUserRole(string roleName)
        {
            Guid roleId = Guid.NewGuid();
            AspNetRole role = new AspNetRole
            {
                Name = roleName,
                Id = roleId.ToString()
            };
            await _aspNetContext.AspNetRoles.AddAsync(role);
            if (_ = await _aspNetContext.SaveChangesAsync() >= 0)
            {
                return role;
            }

            return null;
        }

        public async Task<bool> CheckRoleNameExist(string name)
        {
            AspNetRole? role = await _aspNetContext.AspNetRoles.Where(u => u.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (role != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckRoleExist(string roleId)
        {
            AspNetRole role = await _aspNetContext.AspNetRoles.Where(u => u.Id == roleId).FirstOrDefaultAsync();
            if (role != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> SaveUpdatedRoleNameByIdAsync(string roleId, string roleName)
        {
            AspNetRole? role = await _aspNetContext.AspNetRoles.Where(r => r.Id == roleId).FirstAsync();
            if (role != null)
            {
                role.Name = roleName;

                return await _aspNetContext.SaveChangesAsync() >= 0;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> SaveAssignedUsersByIdsAsync(string roleId, List<string> aspIds)
        {
            List<AspNetUser> users = await _aspNetContext.AspNetUsers.Where(u => aspIds.Contains(u.Id)).ToListAsync();
            if (users.Count() > 0)
            {
                AspNetRole role = await _aspNetContext.AspNetRoles.FirstAsync(r => r.Id == roleId);
                users.ForEach(u => role.Users.Add(u));
                return await _aspNetContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> SaveRemovedUsersByIdsAsync(string roleId, List<string> aspIds)
        {
            List<AspNetUser> users = await _aspNetContext.AspNetUsers.Where(u => aspIds.Contains(u.Id)).ToListAsync();
            if (users.Count() > 0)
            {
                AspNetRole role = await _aspNetContext.AspNetRoles.FirstAsync(r => r.Id == roleId);
                users.ForEach(u => role.Users.Remove(u));
                return await _aspNetContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        //TODO:Employee roles

        public async Task<bool> SaveUpdatedEmployeeRolesAsync(int employeeId, List<string> roleIdList)
        {

            EpeEmployee? oEmployee = await _aspNetContext.EpeEmployees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();
            if (oEmployee != null)
            {
                UsrAccount user = await _aspNetContext.UsrAccounts.Where(u => u.Id == oEmployee.AccountFk).FirstAsync();
                if (user != null)
                {
                    oEmployee.account = user;
                }
                AspNetUser? aspNetUser = await _aspNetContext.AspNetUsers.Where(a => a.UserName == user.UserName).FirstOrDefaultAsync();
                if (aspNetUser != null)
                {
                    string userUuid = aspNetUser.Id;
                }
            }
            return false;
        }

        public Task<bool> SaveRemovedEmployeeRolesAsync(int employeeId, List<string> roleIdList)
        {
            //TODO: This need to 
            throw new NotImplementedException();
        }
    }
}
