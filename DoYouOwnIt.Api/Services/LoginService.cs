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
        private readonly ILogger<LoginService> _logger;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly int _refreshTokenSize;

        public LoginService(SignInManager<ApplicationUser> signInManager, IConfiguration config, 
            UserManager<ApplicationUser> userManager, ILogger<LoginService> logger)
        {
            _signInManager = signInManager;
            _config = config;
            _userManager = userManager;

            _refreshTokenSize = int.TryParse(_config["RefreshTokenSizeBytes"], out var size) ? size : 64;
            _logger = logger;
        }

        /* ValidateRefreshTokenAsync(Guid userId, string refreshToken):
         * Validates the refresh token that is being currently used by the user when they are logged in.
         * If the user doesn't exist, the refreshToken is different from the one stored in the database, or the 
         * RefreshTokenExpirationTime is past the current time, the method will return a null and log the user
         * out.
         */

        private async Task<ApplicationUser?> ValidateRefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
        /* GenerateRefreshToken():
         * Creates a new RefreshToken based on the size of the _refreshTokenSize variable.
         *
         */


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[_refreshTokenSize];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /* GenerateandSaveRefreshTokenAsync(ApplicaitonUser user):
         * -------
         * Creates a new RefreshToken adding it to the User entity on the UserDatabase and
         * adding an expiration date of a week to the user database.
         *
         */

        private async Task<string> GenerateandSaveRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);
            return refreshToken;
        }

        /* Create Token:
         *--------
         *Creates a new JWT Access Token.
         *
         */
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

        /* RefreshTokensAsync(RefreshTokenRequest request) 
         * --------
         * Checks to see if the RefreshToken is valid. If not, it sends out a false success
         * to the client and logs the user out.
         * 
         * Else, it sends a successful requeest with a new accessToken set to expire in 
         * two minutes.
         */

        public async Task<LoginResponse> RefreshTokensAsync(string refreshToken)
        {
            var user = await ValidateRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "User does not exist or refreshtoken no longer exists."
                };
            }

            var jwt = await CreateToken(user);

            return new LoginResponse
            {
                Success = true,
                Token = jwt,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(2)
            };
        }

        /* LoginAsync(LoginRequest request):
         * Checks to see if the user is able to log in properly.
         * 
         * If the user is not found or the if the password or username is invalid the 
         * methodd returns a false success.
         * 
         * If the user is able to log in, the method creates a new access and refresh token
         * and sends them as a LoginResponse, where the information is then either stored in
         * the database or inside the client's localstorage.
         *
         */

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
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(2)
            };
        }
    }
}
