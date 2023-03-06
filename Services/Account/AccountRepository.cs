using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentryBex.Database;
using SentryBex.Dtos;
using SentryBex.Models;
using SentryBex.Models.UsrSchemes;

namespace SentryBex.Services.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AspNetContext _aspContext;
        private readonly AppDbContext _appContext;

        public AccountRepository(AspNetContext aspContext, AppDbContext appContext)
        {
            _aspContext = aspContext;
            _appContext = appContext;
        }



        public async Task<bool> CreateAccountAsync(EpeEmployeeCreateDto createAccountBody)
        {
            bool retVal = false;
            using (_appContext)
            {
                UsrAccount account = new UsrAccount
                {
                    UserName = createAccountBody.Email,
                    Password = createAccountBody.Password,
                    SamAccountName = createAccountBody.SamAccountName,
                    PasswordSalt = createAccountBody.PasswordSalt,
                    Status = createAccountBody.Status,
                };

                await _appContext.UsrAccounts.AddRangeAsync(account);
                if(await _appContext.SaveChangesAsync()>0) retVal = true;
            }

            return retVal; 
        }

        public async Task<IEnumerable<UsrAccount>> GetAllAccountsAsync()
        {
            return await _appContext.UsrAccounts.ToListAsync();
        }

        public async Task<bool> CheckEmailAccountExist(EpeEmployeeCreateDto createAccountBody)
        {
            
            UserAccount? account = await _appContext.UsrAccounts.FirstOrDefaultAsync(a => a.UserName == createAccountBody.Email);
            if (account != null)
            {
                return true;
            }
            return false;
        }



    }
}
