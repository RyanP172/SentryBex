using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using SentryBex.Services;
using SentryBex.Services.Logger;
using SentryBex.Models.EpeSchemes;

namespace SentryBex.Controllers
{
    /// <summary>
    /// This Controller a collection of APIs which is focus on processing info for Show Rooms, and future APIs relate 
    /// to Show Rooms manipulation should be added here.
    /// API Function summary:
    /// 1. List all the show rooms
    /// 2. Acquire signle show room by its id
    /// 3. Update single show room info 
    /// </summary>
    [EnableCors("SentryBexCORSRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowRoomController : ControllerBase
    {
        private readonly IEpeEmployeeRepository _epeEmployeeRepository;
        private readonly ILoggerRepository _loggerRepository;

        public ShowRoomController(
            IEpeEmployeeRepository epeEmployeeRepository,
            ILoggerRepository loggerRepository
            )
        {
            _loggerRepository = loggerRepository ??
                throw new ArgumentNullException(nameof(loggerRepository));
            _epeEmployeeRepository = epeEmployeeRepository ??
                throw new ArgumentNullException(nameof(epeEmployeeRepository));
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetShowRooms()
        {
            var showRoomsFromRepo = await _epeEmployeeRepository.GetEprShowroomsAsync();

            if (showRoomsFromRepo.Count() > 0)
            {
                return Ok(showRoomsFromRepo);
            }
            else
            {
                return NotFound("Show Room list is empty");
            }
        }

        [HttpGet("{Id}", Name = "GetShowRoomById")]
        [HttpHead("{Id}")]
        public async Task<IActionResult> GetShowRoomById(int Id)
        {
            if (await _epeEmployeeRepository.ShowRoomExistAsync(Id))
            {
                var showRoomFromRepo = await _epeEmployeeRepository.GetEprShowroomByIdAsync(Id);
                return Ok(showRoomFromRepo);
            }
            return NotFound($"Show Room {Id} is not found");
        }

        [EnableCors("SentryBexCORSRules")]
        [HttpPatch("{Id}", Name = "UpdateShowRoomById")]
        [Route("id")]

        public async Task<IActionResult> UpdateShowRoomById([FromRoute] int Id, EprShowroom updateShowroom)
        {
            if (await _epeEmployeeRepository.ShowRoomExistAsync(Id))
            {
                if (updateShowroom.State.Length > 10)
                {
                    return BadRequest("State length should below with 10 characters");
                }
                if (await _epeEmployeeRepository.SaveUpdatedShowroomAsync(updateShowroom))
                {
                    await _loggerRepository.RecordLog(
                        "cc468b6f-b0f5-4474-af6a-7d1cf263b862",
                        "Update",
                        $"cc468b6f-b0f5-4474-af6a-7d1cf263b862 success update Show Room info, new info is - " +
                                "Name: " + $"{updateShowroom.Name} " + " " +
                                "Load Day: " + $"{updateShowroom.LoadDay} " + " " +
                                "Monthly Budget: " + $"{updateShowroom.MonthlyBudget} " + " " +
                                "Order Prefix: " + $"{updateShowroom.OrderPrefix} " + " " +
                                "Default Consultant: " + $"{updateShowroom.DefaultConsultantFk} " + " " +
                                "Shop Code: " + $"{updateShowroom.ShopCode} " + " " +
                                "State: " + $"{updateShowroom.State} " + " ",
                        "success"
                        );
                    return StatusCode(StatusCodes.Status200OK, updateShowroom);
                }

            }

            return NotFound($"Show room {Id} is not found");
        }


    }
}
