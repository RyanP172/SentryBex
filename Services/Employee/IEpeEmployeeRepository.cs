using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SentryBex.Dtos;
using SentryBex.Models;
using SentryBex.Models.EpeSchemes;
using System.Collections;

namespace SentryBex.Services
{
    public interface IEpeEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEpeEmployeesAsync();
        Task<IdentityUser> GetAspNetUserByIdAsync(string uuid);
        Task<List<IdentityRole>> GetPermissionListAsync();
        Task<Employee> GetEpeEmployeesByIdAsync(int Id);
        Task<bool> EmployeeExistAsync(int Id);
        Task<Employee> GetEpeEmployeesByIdAsync(int Id, Employee updateEmployee);
        Task<IEnumerable<EprShowroom>> GetEprShowroomsAsync();
        Task<EprShowroom> GetEprShowroomByIdAsync(int Id);
        Task<bool> ShowRoomExistAsync(int Id);
        Task<bool> PermissionExistAsync(string Id);
        Task<IEnumerable> GetPermissionByIdAsync(string id);

        Task<bool> CheckEmployeeShowroomLinkExistAsync(int employeeId, int showroomId);
        Task<bool> SaveUpdatedEmployeeAsync(EpeEmployee updateEmployee);
        Task<bool> SaveUpdatedShowroomAsync(EprShowroom updateShowroom);
        Task<bool> SaveUpdatedEmployeeShowroomLinkAsync(int employeeId, List<long> showroomIdList);
        Task<bool> SaveRemovedEmployeeShowroomLinkAsync(int employeeId, List<long> showroomIdList);
        Task<bool> SaveUpdatedEmployeeActivationStatus(int employeeId, string status);
        Task<bool> SaveUpdatedAspNetUserRoleByIdAsync(string uuid, List<string> roleId);
        Task<bool> SaveRemovedAspNetUserRoleByIdAsync(string uuid, List<string> roleIds);

        //TODO: Ryan's task
        Task<bool> SaveCreatedEmployeeAsync(EpeEmployeeCreateDto createEmployeeBody);
        /*Task<bool> SaveLinkedCreatedEmployeeAsync(EpeEmployeeCreateDto linkedEmployee);*/


    }
}
