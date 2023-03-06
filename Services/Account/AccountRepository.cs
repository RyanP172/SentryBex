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

        
        public async Task<UsrAccount> CreateAccountAsync(UsrAccountCreateDto createAccountBody)
        {
            var account = new UsrAccount{
                UserName = createAccountBody.Email, 
                Password=createAccountBody.Password,
                SamAccountName=createAccountBody.SamAccountName,
                PasswordSalt =createAccountBody.PasswordSalt,
                Status = createAccountBody.Status,
                };

            await _appContext.UsrAccounts.AddRangeAsync(account);
            await _appContext.SaveChangesAsync();

                           

            return account; 
        }

        public async Task<IEnumerable<UsrAccount>> GetAllAccountsAsync()
        {
            return await _appContext.UsrAccounts.ToListAsync();
        }



    }
}
