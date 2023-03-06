using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SentryBex.Dtos;
using SentryBex.Models.UsrSchemes;
using SentryBex.Services.Account;

namespace SentryBex.Controllers
{
    
    /// <summary>
    

    /// </summary>
    

    [EnableCors("SentryBexCORSRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return  Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] EpeEmployeeCreateDto registerBody)
        {
            if (await _accountRepository.CheckEmailAccountExist(registerBody))
            {
                return BadRequest(new { status = 400, message = $"Account email {registerBody.Email} already existed" });
            }
            var account = await _accountRepository.CreateAccountAsync(registerBody);
            return Ok(account);
        }
    }
}
