using Azure.Core;
using DoYouOwnIt.Shared.Models.Login;
using DoYouOwnIt.Shared.Models.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoYouOwnIt.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginService(SignInManager<ApplicationUser> signInManager, IConfiguration config, 
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _config = config;
            _userManager = userManager;
        }

        private async Task<ApplicationUser?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task<string> GenerateandSaveRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);
            return refreshToken;
        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSecurityKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToInt32(_config["JwtExpiryInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _config["JwtIssuer"],
                audience: _config["JwtAudience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse?> RefreshTokensAsync(RefreshTokenRequest request)
        {

            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "User does not exist."
                };
            }

            var jwt = await CreateToken(user);

            var refreshToken = await GenerateandSaveRefreshTokenAsync(user);

            return new LoginResponse
            {
                Success = true,
                Token = jwt,
                RefreshToken = refreshToken
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Username, request.Password, false, false);

            if (!result.Succeeded)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "Invalid username or password."
                };
            }

            var user = await _signInManager.UserManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "User does not exist."
                };
            }

            var jwt = await CreateToken(user);

            var refreshToken = await GenerateandSaveRefreshTokenAsync(user);

            return new LoginResponse
            {
                Success = true,
                Token = jwt,
                RefreshToken = refreshToken
            };
        }

        
    }
}
