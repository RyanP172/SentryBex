using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using SentryBex.Database;
using SentryBex.Models;
using System.Collections.Generic;
using System.Linq;
using SentryBex.Utilitty;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using SentryBex.Models.AspSchemes;
using SentryBex.Models.EpeSchemes;
using SentryBex.Models.UsrSchemes;
using SentryBex.Dtos;
using System;
using System.Security.Principal;

namespace SentryBex.Services
{
    public class EpeEmployeeRepository : IEpeEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly AspNetContext _aspNetContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EpeEmployeeRepository(
            AppDbContext context, 
            AspNetContext aspNetContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _aspNetContext = aspNetContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Employee>> GetEpeEmployeesAsync()
        {
            IQueryable<Employee> query = from epe in _context.EpeEmployees
                        join usr in _context.UsrAccounts on epe.AccountFk equals usr.Id
                        select new EpeEmployee
                        {   Id = epe.Id, 
                            FirstName = epe.FirstName, 
                            LastName = epe.LastName,
                            MiddleName = epe.MiddleName,
                            Dob = epe.Dob,
                            Created = epe.Created,
                            Modified = epe.Modified,
                            Code = epe.Code,
                            IsContractor = epe.IsContractor,
                            MaxLeadCount = epe.MaxLeadCount,
                            MonthlyBudget = epe.MonthlyBudget,
                            account = usr
                        };

            var result =await query.ToListAsync();
            return result;
        }


        public async Task<Employee> GetEpeEmployeesByIdAsync(int Id)
        {
            EpeEmployee? oEmployee = await _aspNetContext.EpeEmployees.Where(e => e.Id == Id).FirstOrDefaultAsync();
            if (oEmployee != null)
            {
                //Find show-rooms link to the employee and store as list  
                List<int> showRoomIds = await _aspNetContext.EpeEmployeeShowroomLinks.Where(e => e.EmployeeFk == Id).Select(e => e.ShowroomFk).ToListAsync();

                List<EprShowroom> oshowrooms = await _aspNetContext.EprShowrooms.Where(k => showRoomIds.Contains(k.Id)).ToListAsync();
                if (oshowrooms.Count() > 0)
                {
                    oEmployee.showRooms.AddRange(oshowrooms.ToList());
                }

                //Find roles
                UsrAccount user = await _aspNetContext.UsrAccounts.Where(u => u.Id == oEmployee.AccountFk).FirstAsync();
                if (user != null)
                {
                    oEmployee.account = user;
                }
                AspNetUser? aspNetUser = await _aspNetContext.AspNetUsers.Select(a => new AspNetUser
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    Roles = a.Roles.Select(ar => new AspNetRole { Id = ar.Id, Name = ar.Name }).ToList()
                }).Where(a => a.UserName == user.UserName).FirstOrDefaultAsync();
                if (aspNetUser != null)
                {
                    oEmployee.netUser.Id = aspNetUser.Id;
                    oEmployee.netUser.UserName = aspNetUser.UserName;
                    List<AspNetRole> userRoles = aspNetUser.Roles.ToList();
                    if (userRoles.Count() > 0)
                    {
                        oEmployee.permissions.AddRange(userRoles.ToList());
                    }
                }

                return oEmployee;
            }
            return null;
        }

        public async Task<Employee> GetEpeEmployeesByIdAsync(int Id, Employee updateEmployee)
        {
            EpeEmployee? Employee = await _aspNetContext.EpeEmployees.Where(e => e.Id == Id).FirstOrDefaultAsync();
            if (updateEmployee != null)
            {
                Employee = (EpeEmployee)updateEmployee;
                return Employee;
            }
            return null;

        }

        public async Task<IEnumerable<EprShowroom>> GetEprShowroomsAsync()
        {
            IQueryable<EprShowroom> result = _aspNetContext.EprShowrooms;
            return await result.ToListAsync();
        }
        public async Task<bool> EmployeeExistAsync(int Id)
        {
            return await _context.EpeEmployees.AnyAsync(e => e.Id == Id);
        }

        public async Task<bool> ShowRoomExistAsync(int Id)
        {
            return await _context.EprShowrooms.AnyAsync(s => s.Id == Id);
        }

        public async Task<EprShowroom> GetEprShowroomByIdAsync(int Id)
        {
            EprShowroom room = await _context.EprShowrooms.Where(r => r.Id == Id).FirstAsync();

            if (room == null)
            {
                return null;
            }
            return room;
        }

        public async Task<List<IdentityRole>> GetPermissionListAsync()
        {
           var list = await _roleManager.Roles.ToListAsync();
            return list;
        }
        
        public async Task<bool> PermissionExistAsync(string Id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            return role != null;

        }

        public async Task<IEnumerable> GetPermissionByIdAsync(string id)
        {
            var result = _aspNetContext.AspNetRoles
                            .Where(r => r.Id == id)
                            .Select(r => new
                            {
                                roleId = r.Id,
                                roleName = r.Name,
                                relateUsers = r.Users.Select(u => new
                                {
                                    id = u.Id,
                                    email = u.Email,
                                    usrAccId = (_aspNetContext.UsrAccounts.Where(a => a.UserName == u.Email).Select(a => a.Id)).FirstOrDefault(),
                                    epeId = (_aspNetContext.EpeEmployees
                                            .Where
                                                (e => e.AccountFk == (
                                                    _aspNetContext.UsrAccounts.Where(a => a.UserName == u.Email)
                                                    .Select(a => a.Id)).FirstOrDefault()
                                                ))
                                            .Select(e => e.Id).FirstOrDefault()
                                })
                            });

            if (result != null)
            {
                return await result.ToListAsync();
            }
            return null;
        }


        public async Task<bool> SaveUpdatedShowroomAsync(EprShowroom updateShowroom)
        {
            EprShowroom _updateShowroom = updateShowroom;
            EprShowroom? oShowroom = await _context.EprShowrooms.Where(s => s.Id == updateShowroom.Id).FirstOrDefaultAsync();
            _updateShowroom.DeepCopyObjects(oShowroom);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> SaveUpdatedEmployeeAsync(EpeEmployee updateEmployee)
        {
            updateEmployee.Modified = DateTime.Now;
            EpeEmployee _updateEmployee = updateEmployee;
            EpeEmployee? oEmployee = await _context.EpeEmployees.Where(e => e.Id == _updateEmployee.Id).FirstOrDefaultAsync();

            _updateEmployee.DeepCopyObjects(oEmployee);

            return await _context.SaveChangesAsync() >= 0;
        }


        public async Task<bool> CheckEmployeeShowroomLinkExistAsync(int employeeId, int showroomId)
        {
            return await _context.EpeEmployeeShowroomLinks.AnyAsync(ln => ln.ShowroomFk == showroomId && ln.EmployeeFk == employeeId);
        }
        public async Task<bool> SaveUpdatedEmployeeShowroomLinkAsync(int employeeId, List<long> showroomIdList)
        {
            List<EpeEmployeeShowroomLink> showRoomList = new List<EpeEmployeeShowroomLink>();
            //showRoomList = _context.EpeEmployeeShowroomLinks.Where(a => showroomIdList.Contains(a.ShowroomFk) && a.EmployeeFk == employeeId).ToList();


            foreach (long showroomId in showroomIdList)
            {
                EpeEmployeeShowroomLink link = new EpeEmployeeShowroomLink();
                link.ShowroomFk = (int)showroomId;
                link.EmployeeFk = employeeId;
                showRoomList.Add(link);
            }
            await _context.EpeEmployeeShowroomLinks.AddRangeAsync(showRoomList.ToList());
            return await _context.SaveChangesAsync() >= 0;
        }



        public async Task<bool> SaveRemovedEmployeeShowroomLinkAsync(int employeeId, List<long> showroomIdList)
        {
            List<EpeEmployeeShowroomLink> showRoomList = await _aspNetContext.EpeEmployeeShowroomLinks.Where(a => showroomIdList.Contains(a.ShowroomFk) && a.EmployeeFk == employeeId).ToListAsync();
            List<EpeEmployeeShowroomLink> existedShowRoomList = await _aspNetContext.EpeEmployeeShowroomLinks.Where(a => a.EmployeeFk == employeeId).ToListAsync();
            if (existedShowRoomList.Count == 1 || existedShowRoomList.Count - showRoomList.Count == 0)
            {
                return false;
            }
            else
            {
                _aspNetContext.EpeEmployeeShowroomLinks.RemoveRange(showRoomList);
                return await _aspNetContext.SaveChangesAsync() >= 0;
            }
        }

        public async Task<bool> SaveUpdatedEmployeeActivationStatus(int employeeId, string status)
        {
            EpeEmployee? oEmployee = await _aspNetContext.EpeEmployees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();
            if (oEmployee != null)
            {
                UsrAccount user = await _aspNetContext.UsrAccounts.Where(u => u.Id == oEmployee.AccountFk).FirstAsync();
                user.Status = status;
                return await _aspNetContext.SaveChangesAsync() >= 0;
            }
            return false;
        }


        public async Task<bool> SaveUpdatedAspNetUserRoleByIdAsync(string uuid, List<string> roleIds)
        {
            List<AspNetRole>? roles = await _aspNetContext.AspNetRoles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
            if (roles.Count() > 0)
            {
                AspNetUser user = await _aspNetContext.AspNetUsers.FirstAsync(a => a.Id == uuid);
                roles.ForEach(a => user.Roles.Add(a));
                return await _aspNetContext.SaveChangesAsync() > 0;

            }
            return false;

        }

        public async Task<bool> SaveRemovedAspNetUserRoleByIdAsync(string uuid, List<string> roleIds)
        {
            List<AspNetRole>? roles = await _aspNetContext.AspNetRoles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
            if (roles.Count() > 0)
            {
                var roleCounter = 0;
                AspNetUser user = await _aspNetContext.AspNetUsers.FirstAsync(a => a.Id == uuid);
                roles.ForEach(a => { user.Roles.Remove(a); roleCounter = user.Roles.Count(); });
                if (roleCounter == 0)
                {
                    roles.ForEach(a => { user.Roles.Add(a); });
                    return false;
                }
                else
                {
                    return await _aspNetContext.SaveChangesAsync() > 0;
                }
            }
            return false;
        }

        public async Task<IdentityUser> GetAspNetUserByIdAsync(string uuid)
        {
            IdentityUser user = await _userManager.FindByIdAsync(uuid);
            return user;
        }

        public async Task<bool> SaveCreatedEmployeeAsync(EpeEmployeeCreateDto _employee)
        {
            bool retVal = false;            
            using (_context)
            {
                UsrAccount account = new UsrAccount
                {
                    UserName = _employee.Email,
                    Password = _employee.Password,
                    SamAccountName = _employee.SamAccountName,
                    PasswordSalt = _employee.PasswordSalt,
                    Status = _employee.Status,
                };
                await _context.UsrAccounts.AddRangeAsync(account);
                if (await _context.SaveChangesAsync() > 0)
                {                    
                    EpeEmployee employee = new EpeEmployee
                    {
                        FirstName = _employee.FirstName,
                        LastName = _employee.LastName,
                        MiddleName = _employee.MiddleName,
                        Dob = _employee.Dob,
                        Code = _employee.Code,
                        IsContractor = _employee.IsContractor,
                        ContractorTypeFk = _employee.ContractorTypeFk,
                        DefaultShowroomFk = _employee.DefaultShowroomFk,
                        MaxLeadCount = _employee.MaxLeadCount,
                        MonthlyBudget = _employee.MonthlyBudget,
                        AccountFk = account.Id,

                    };

                    await _context.EpeEmployees.AddAsync(employee);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        EpeEmployeeShowroomLink showroomLink = new EpeEmployeeShowroomLink
                        {
                            ShowroomFk = _employee.DefaultShowroomFk,
                            EmployeeFk = _employee.CompanyId
                        };
                        await _context.EpeEmployeeShowroomLinks.AddRangeAsync(showroomLink);
                        EpeEmployeeCompanyLink companyLink = new EpeEmployeeCompanyLink
                        {
                            CompanyFk = _employee.CompanyId,
                            EmployeeFk = employee.Id
                        };
                        await _context.EpeEmployeeCompanyLinks.AddRangeAsync(companyLink);
                        EpeEmployeeGroupLink groupLink = new EpeEmployeeGroupLink
                        {
                            EmployeeFk = employee.Id,
                            GroupFk = 4,

                        };
                        await _context.EpeEmployeeGroupLinks.AddRangeAsync(groupLink);
                        if (await _context.SaveChangesAsync() > 0) retVal = true;

                    }
                }

                    
            }
            return retVal;

        }


    }
}
