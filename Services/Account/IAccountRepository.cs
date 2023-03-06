using SentryBex.Dtos;
using SentryBex.Models;
using SentryBex.Models.UsrSchemes;

namespace SentryBex.Services.Account
{
    public interface IAccountRepository
    {
        
        Task<UsrAccount> CreateAccountAsync(UsrAccountCreateDto createAccountBody);
        Task<IEnumerable<UsrAccount>> GetAllAccountsAsync();
        




    }
}
