using SentryBex.Models.AspSchemes;
using System.Collections;

namespace SentryBex.Services
{
    public interface IUserRoleRepository
    {
        Task<AspNetRole> CreateNewUserRole(string name);
        Task<bool> CheckRoleNameExist(string name);
        Task<bool> CheckRoleExist(string roleId);
        Task<IEnumerable> GetAspNetUsersInfoAsync();
        Task<bool> SaveUpdatedRoleNameByIdAsync(string roleId, string roleName);
        Task<bool> SaveAssignedUsersByIdsAsync(string roleId, List<string> aspIds);
        Task<bool> SaveRemovedUsersByIdsAsync(string roleId, List<string> aspIds);
        //TODO:
        Task<bool> SaveUpdatedEmployeeRolesAsync(int employeeId, List<string> roleIdList);
        Task<bool> SaveRemovedEmployeeRolesAsync(int employeeId, List<string> roleIdList);
    }
}
