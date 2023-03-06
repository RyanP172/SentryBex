using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SentryBex.Models;
using SentryBex.Services;
using SentryBex.Services.Logger;
using System;

namespace SentryBex.Controllers
{
    /// <summary>
    /// This Controller a collection of APIs which is focus on processing Role's info, and future APIs relate 
    /// to Role manipulation should be added here.
    /// API Function summary:
    /// 1. Acquire all permission 
    /// 2. Acquire single permission by Id
    /// 3. Create new permission by providing a role name
    /// 4. Assign users to a role
    /// 5. Remove users from a role
    /// </summary>
    /// 

    [EnableCors("SentryBexCORSRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IEpeEmployeeRepository _epeEmployeeRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILoggerRepository _loggerRepository;

        public RoleController(
            IEpeEmployeeRepository epeEmployeeRepository,
            IUserRoleRepository userRoleRepository,
            ILoggerRepository loggerRepository
        )
        {
            _epeEmployeeRepository = epeEmployeeRepository ?? throw new ArgumentNullException(nameof(epeEmployeeRepository));
            _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
            _loggerRepository = loggerRepository ?? throw new ArgumentNullException(nameof(loggerRepository));
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetPermissions()
        {
            var roleListFromRepo = await _epeEmployeeRepository.GetPermissionListAsync();
            if (roleListFromRepo.Count() > 0)
            {
                return Ok(roleListFromRepo);
            }
            return NotFound("No available role for user assign at the moment");
        }



        [EnableCors("SentryBexCORSRules")]
        [HttpPost(Name = "CreateNewRole")]
        [HttpHead("{roleName}")]
        public async Task<IActionResult> CreateNewRole([FromBody] string roleName)
        {
            if (await _userRoleRepository.CheckRoleNameExist(roleName))
            {
                return BadRequest(new { status = 400, message = "This role name is duplicated" });
            }
            var role = await _userRoleRepository.CreateNewUserRole(roleName);
            if (role != null)
            {
                await _loggerRepository.RecordLog(
                       "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                       "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 Created a new role - {roleName}", "success"
                       );
                return Ok(new { status = 200, message = "New role has been added", response = role });
            }
            else
            {
                await _loggerRepository.RecordLog(
                       "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                       "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 Created a new role - {roleName}", "failed"
                       );
                return BadRequest(new { status = 400, message = "Role creation failed" });
            }

        }

        [HttpGet("aspuser/{uuid}", Name = "GetAspUsersByUuid")]
        [HttpHead("{uuid}")]
        public async Task<IActionResult> GetAspUsersByUuid(string uuid)
        {
            var result = await _epeEmployeeRepository.GetAspNetUserByIdAsync(uuid);
            return Ok(result);
        }

        [HttpGet("{uuid}", Name = "GetRoleById")]
        [HttpHead("{uuid}")]
        public async Task<IActionResult> GetPermissionById([FromRoute] string uuid)
        {
            if (await _epeEmployeeRepository.PermissionExistAsync(uuid))
            {
                var roleFromRepo = await _epeEmployeeRepository.GetPermissionByIdAsync(uuid);
                return Ok(roleFromRepo);
            }
            return NotFound("The permission you are looking for is not exist");
        }

        [HttpPatch("{uuid}", Name = "UpdatePermissionById")]
        [HttpHead("{uuid}")]
        public async Task<IActionResult> UpdatePermissionById(string uuid, [FromBody] string roleName)
        {
            //TODO: Update the role name
            if (await _userRoleRepository.CheckRoleExist(uuid))
            {
                if (!await _userRoleRepository.CheckRoleNameExist(roleName))
                {
                    bool result = await _userRoleRepository.SaveUpdatedRoleNameByIdAsync(uuid, roleName);
                    if (result)
                    {
                        await _loggerRepository.RecordLog(
                       "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                       "Update", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 changed the role name to - {roleName}", "success"
                       );
                        return Ok(new { status = 200, message = "Role name has been changed", response = roleName });
                    }

                    await _loggerRepository.RecordLog(
                   "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                   "Update", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed to change the role name to - {roleName}", "failed"
                   );
                    return BadRequest(new { status = 400, message = "Role name change failed" });


                }
                else
                {
                    return BadRequest(new { status = 400, message = "Role name already existed" });
                }
            }
            else
            {
                return BadRequest(new { status = 400, message = "Role does not exist" });
            }


        }

        [EnableCors("SentryBexCORSRules")]
        [HttpPost("{roleId}/aspuser", Name = "AssignAspUsersToRole")]
        [HttpHead("{roleId}")]
        public async Task<IActionResult> AssignAspUsersToRole(string roleId, [FromBody] List<string> aspUserIds)
        {
            if (await _userRoleRepository.CheckRoleExist(roleId))
            {
                //TODO:Add users to role 
                var result = await _userRoleRepository.SaveAssignedUsersByIdsAsync(roleId, aspUserIds);
                if (result)
                {
                    string idString = string.Join(", ", aspUserIds);
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success assign Users - id: {idString} to Role - id: {roleId}", "success"
                        );
                    return Ok(new { status = 200, message = $"Selected users have been assigned to this role {roleId}", result });
                }
                else
                {
                    string idString = string.Join(", ", aspUserIds);
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed to assign Users - id: {idString} to Role - id: {roleId}", "failed"
                        );
                    return BadRequest(new { status = 400, message = $"The user may not exist or already signed to this role - {roleId}", result });
                }

            }
            else
            {
                return NotFound(new { status = 404, message = "The role does not exist" });
            }

        }

        [EnableCors("SentryBexCORSRules")]
        [HttpDelete("{roleId}/aspuser", Name = "RemoveAspUsersFromRole")]
        [HttpHead("{roleId}")]
        public async Task<IActionResult> RemoveAspUsersFromRole(string roleId, [FromBody] List<string> aspUserIds)
        {
            if (await _userRoleRepository.CheckRoleExist(roleId))
            {
                //TODO:Remove users from role 
                string idString = string.Join(", ", aspUserIds);
                var result = await _userRoleRepository.SaveRemovedUsersByIdsAsync(roleId, aspUserIds);
                if (result)
                {
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Delete", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success remove Users - id: {idString} from Role - id: {roleId}", "success"
                        );
                    return Ok(new { status = 200, message = $"Selected users have been removed from this role {roleId}", result });
                }


                await _loggerRepository.RecordLog(
                    "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                    "Delete", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed to assign Users - id: {idString} to Role - id: {roleId}", "failed"
                    );
                return BadRequest(new { status = 400, message = $"The user may not exist or already removed from this role - {roleId}", result });


            }
            return NotFound("The role does not exist");

        }

        [EnableCors("SentryBexCORSRules")]
        [HttpPost("aspuser/{uuid}/aspuserrole", Name = "AddAspUserRolesByUuid")]
        [HttpHead("{uuid}")]
        public async Task<IActionResult> AddAspUserRolesByUuid(string uuid, [FromBody] List<string> roleIds)
        {
            string roleIdString = string.Join(", ", roleIds);
            var result = await _epeEmployeeRepository.SaveUpdatedAspNetUserRoleByIdAsync(uuid, roleIds);
            if (result)
            {
                /*return Ok(new 
                { 
                    status: 200,
                    message: $"Roles has been assigned to user {uuid}",
                });*/

                await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success assign Roles - id: {roleIdString} to Employee - id: {uuid}", "success"
                        );
                return Ok(new { status = 200, message = $"Roles have been assigned to user {uuid}", result });
            }

            await _loggerRepository.RecordLog(
                    "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                    "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed assign Roles - id: {roleIdString} to Employee - id: {uuid}", "failed"
                    );
            return BadRequest(new { status = 400, message = $"The role may not exist or already signed to this user - {uuid}", result });


        }

        [EnableCors("SentryBexCORSRules")]
        [HttpDelete("aspuser/{uuid}/aspuserrole", Name = "RemoveAspUserRolesByUuid")]
        [HttpHead("{uuid}")]
        public async Task<IActionResult> RemoveAspUserRolesByUuid(string uuid, [FromBody] List<string> roleIds)
        {
            string roleIdString = string.Join(", ", roleIds);
            if (await _epeEmployeeRepository.SaveRemovedAspNetUserRoleByIdAsync(uuid, roleIds))
            {

                await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Delete", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success remove Roles - id: {roleIdString} from Employee - id: {uuid}", "success"
                        );
                /*return Ok($);*/
                return Ok(new
                {
                    StatusCode = 200,
                    Success = true,
                    Message = $"Roles were removed from user {uuid}"
                });
            }


            await _loggerRepository.RecordLog(
                       "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                       "Delete", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed remove Roles - id: {roleIdString} from Employee - id: {uuid}", "failed"
                       );
            /*return BadRequest("Role remove failed");*/
            return BadRequest(new
            {
                StatusCode = 400,
                Success = false,
                Message = $"Role remove failed. A user must have at least role and / or the roles you want to remove were not assigned to this user"
            });
        }

        [EnableCors("SentryBexCORSRules")]
        [HttpGet("aspuser", Name = "GetAspNetUsersInfo")]

        public async Task<IActionResult> GetAspNetUsersInfo()
        {
            var result = await _userRoleRepository.GetAspNetUsersInfoAsync();
            return Ok(new { status = 200, response = result });
        }

    }
}
