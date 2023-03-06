using SentryBex.Dtos;

namespace SentryBex.Services.Authentication
{
    public interface IAuthenticationRepository
    {
        Task<bool> Register(AspNetUserRegisterDto registerBody);
        Task<bool> Login(LogInDto loginBody);
        Task<bool> CheckEmailExist(AspNetUserRegisterDto registerBody);
        Task<string> AssignJwtToken(LogInDto loginBody);
    }
}
