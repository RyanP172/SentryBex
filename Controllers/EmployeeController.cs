using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SentryBex.Dtos;
using SentryBex.Models.EpeSchemes;
using SentryBex.Services;
using SentryBex.Services.Account;
using SentryBex.Services.Logger;
using SentryBex.Utilitty;

namespace SentryBex.Controllers
{
    /// <summary>
    /// This Controller a collection of APIs which is focus on acquire and processing Employee's info from database, and future APIs relate 
    /// to Employee manipulation should be added here.
    /// API Function summary:
    /// 1. Acquire all employee's info
    /// 2. Acquire one employee's info by id including all the permissions and showrooms link to 
    /// 3. Update one employee's info by id for basic info
    /// 4. Link show room to employee
    /// </summary>
    /// 


    [EnableCors("SentryBexCORSRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEpeEmployeeRepository _epeEmployeeRepository;

        private readonly ILoggerRepository _loggerRepository;
        private readonly IAccountRepository _accountRepository;

        public EmployeeController(
            IEpeEmployeeRepository epeEmployeeRepository,
            ILoggerRepository loggerRepository,
            IAccountRepository accountRepository
            )
        {
            _epeEmployeeRepository = epeEmployeeRepository ?? throw new ArgumentNullException(nameof(epeEmployeeRepository));
            _loggerRepository = loggerRepository ?? throw new ArgumentNullException(nameof(loggerRepository));
            _accountRepository = accountRepository;
        }


        [EnableCors("SentryBexCORSRules")]
        [HttpGet]
        [HttpHead]
        /*[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> GetEmployees()
        {
            var employeesFromRepo = await _epeEmployeeRepository.GetEpeEmployeesAsync();

            if (employeesFromRepo.Count() > 0)
            {
                return Ok(employeesFromRepo);
            }
            else
            {
                return NotFound("Employee list is empty");
            }

        }

        [HttpGet("{Id}", Name = "GetEmployeeById")]
        [HttpHead("{Id}")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            if (await _epeEmployeeRepository.EmployeeExistAsync(Id))
            {
                var employeeFromRepo = await _epeEmployeeRepository.GetEpeEmployeesByIdAsync(Id);
                return Ok(employeeFromRepo);
            }
            return NotFound($"Employee {Id} is not found");

        }


        [EnableCors("SentryBexCORSRules")]
        [HttpPatch("{Id}", Name = "UpdateEmployeeById")]

        public async Task<IActionResult> UpdateEmployeeById(int Id, EpeEmployee updateEmployee)
        {
            if (await _epeEmployeeRepository.EmployeeExistAsync(Id))
            {
                if (updateEmployee.netUser.Id != "")
                {
                    await _epeEmployeeRepository.SaveUpdatedEmployeeAsync(updateEmployee);
                    //TODO: Currently for testing, but will get user based on login info
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Update",
                        $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success update Epe Employee info, new info is - " +
                                "FirstName: " + $"{updateEmployee.FirstName} " + " " +
                                "MiddleName: " + $"{updateEmployee.MiddleName} " + " " +
                                "LastName: " + $"{updateEmployee.LastName} " + " " +
                                "Monthly Budget: " + $"{updateEmployee.MonthlyBudget} " + " " +
                                "Is Contractor: " + $"{updateEmployee.IsContractor} " + " ",
                        "success"
                        );
                    return Ok(new
                    {
                        status = 200,
                        description = "Employee information has been updated",
                        updateEmployee
                    });
                }
                else
                {
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Update", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed update Epe Employee info", "failed"
                        );
                    return BadRequest(new
                    {
                        status = 400,
                        description = "This user has not login his account yet",
                    });
                }

            }
            return NotFound($"Employee {Id} is not found");
        }





        [EnableCors("SentryBexCORSRules")]
        [HttpPost("{employeeId}", Name = "UpdateEmployeeShowroomLink")]
        /* [Route("employeeId")]*/
        public async Task<IActionResult> UpdateEmployeeShowroomLink([FromRoute] int employeeId, [FromBody] List<long> showroomIdList)
        {
            if (await _epeEmployeeRepository.EmployeeExistAsync(employeeId))
            {

                /*foreach (int showroomId in showroomIdList)
                {
                    if (await _epeEmployeeRepository.CheckEmployeeShowroomLinkExistAsync(employeeId, showroomId))
                    {
                        return BadRequest($"The requested show room {showroomId} already bind to this user");
                    }
                    if (!await _epeEmployeeRepository.ShowRoomExistAsync(showroomId))
                    {
                        return BadRequest($"The requested show room {showroomId} does not exist");
                    }
                }*/
                if (await _epeEmployeeRepository.SaveUpdatedEmployeeShowroomLinkAsync(employeeId, showroomIdList))
                {
                    var result = new HttpRequestStatus()
                    {
                        StatusCode = 200,
                        Success = true,
                        Message = "Requested room list has been added to this user"

                    };
                    string numbersString = string.Join(", ", showroomIdList);
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Create", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success assign show rooms - id: {numbersString} to Epe employee - id: {employeeId}", "success"
                        );
                    return Ok(result);
                }

                //dfads
                //Ok("Requested room list has been added to this user");
            }
            return NotFound($"Employee {employeeId} is not found");
        }


        [EnableCors("SentryBexCORSRules")]
        [HttpDelete("{employeeId}", Name = "RemoveEmployeeShowroomLink")]
        /*[Route("employeeId")]*/
        public async Task<IActionResult> RemoveEmployeeShowroomLink([FromRoute] int employeeId, [FromBody] List<long> showroomIdList)
        {

            if (await _epeEmployeeRepository.EmployeeExistAsync(employeeId))
            {
                /*foreach (int showroomId in showroomIdList)
                {
                    if (!await _epeEmployeeRepository.CheckEmployeeShowroomLinkExistAsync(employeeId, showroomId))
                    {
                        return BadRequest($"The requested show room {showroomId} does not bind to this user");
                    }
                    if (!await _epeEmployeeRepository.ShowRoomExistAsync(showroomId))
                    {
                        return BadRequest($"The requested show room {showroomId} does not exist");
                    }
                }*/

                if (await _epeEmployeeRepository.SaveRemovedEmployeeShowroomLinkAsync(employeeId, showroomIdList))
                {
                    string numbersString = string.Join(", ", showroomIdList);
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Delete", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success remove show rooms - id: {numbersString} from Epe employee - id: {employeeId}", "success"
                        );
                    var result = new HttpRequestStatus()
                    {
                        StatusCode = 200,
                        Success = true,
                        Message = "Requested room list has been removed from this user"

                    };
                    return Ok(result);
                }

                await _loggerRepository.RecordLog(
                    "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                    "Delete", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed remove show rooms as employee should have at least one show room binded", "failed"
                    );
                return BadRequest(new { status = 400, message = "Employee should have at least one show room binded" });


            }
            return NotFound($"Employee {employeeId} is not found");

        }

        [EnableCors("SentryBexCORSRules")]
        [HttpPost("{employeeId}/status", Name = "UpdateActivateStatusById")]
        [HttpHead("{employeeId}")]
        /*[Route("{employeeId}/status")]*/
        public async Task<IActionResult> UpdateActivateStatusById(int employeeId, [FromBody] string status)
        {
            if (await _epeEmployeeRepository.EmployeeExistAsync(employeeId))
            {
                if (await _epeEmployeeRepository.SaveUpdatedEmployeeActivationStatus(employeeId, status))
                {
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Update", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 has changed user - id: {employeeId} - status to {status}", "success"
                        );
                    return Ok(new { status = 200, message = "User status has been updated" });
                }
                else
                {
                    await _loggerRepository.RecordLog(
                       "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                       "Update", $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 failed to changed user - id: {employeeId} - status to {status}", "failed"
                       );
                    return StatusCode(500, new { status = 500, message = "Employee status change failed" });
                }

            }
            return NotFound("The employee you are looking for is not exist");
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewEmployee([FromBody] EpeEmployeeCreateDto createEmployee)
        {
            if (await _accountRepository.CheckEmailAccountExist(createEmployee))
            {
                return BadRequest(new { status = 400, message = $"Account email {createEmployee.Email} already existed" });
            }
            var employee = await _epeEmployeeRepository.SaveCreatedEmployeeAsync(createEmployee);
            return Ok(employee);
        }
    }
}
