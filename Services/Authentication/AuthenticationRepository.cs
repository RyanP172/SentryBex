using Microsoft.AspNetCore.Identity;
using SentryBex.Dtos;
using SentryBex.Models;
using System.Text;
using System.Security.Claims;
using SentryBex.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SentryBex.Services.Authentication
{
    /// <summary>
    /// This file is focusing on user authentication and authorisation
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        public AuthenticationRepository(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> CheckEmailExist(AspNetUserRegisterDto registerBody)
        {
            var user = await _userManager.FindByEmailAsync(registerBody.Email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Login(LogInDto loginBody)
        {

            var loginResult = await _signInManager.PasswordSignInAsync(
                loginBody.Email,
                loginBody.Password,
                false,
                false
            );
            if (!loginResult.Succeeded)
            {
                return false;
            }
            return true;

        }

        public async Task<string> AssignJwtToken(LogInDto loginBody)
        {
            //create jwt

            /*Header
            * Payload
            * Signagure*/

            var userDetail = await _userManager.FindByNameAsync(loginBody.Email);

            string signInAlgorithm = SecurityAlgorithms.HmacSha256;
            var claims = new List<Claim>
            {
                //sub - define the user role
                new Claim(JwtRegisteredClaimNames.Sub, userDetail.Id),
                /*new Claim(ClaimTypes.Role, "Admin") or other role types*/
            };


            //Accquire the roles assigned to the user login to the system, then add claimtypes and issue it to the JWT token
            var roleNames = await _userManager.GetRolesAsync(userDetail);
            foreach (var roleName in roleNames)
            {
                var roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }

            //signature
            //get private key
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            //encrypt private key with symmetric
            var signinKey = new SymmetricSecurityKey(secretByte);
            //validation of the encrypted private key
            var signinCredentials = new SigningCredentials(signinKey, signInAlgorithm);

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signinCredentials
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;

        }

        public async Task<bool> Register(AspNetUserRegisterDto registerBody)
        {

            var user = new IdentityUser { UserName = registerBody.Email, Email = registerBody.Email };
            var result = await _userManager.CreateAsync(user, registerBody.Password);
            if (result.Succeeded)
            {
                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }
            return false;
        }
    }
}
