using DoYouOwnIt_Shared.Models.Emails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DoYouOwnIt.Shared.Models.Account
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task AssignRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            await _userManager.AddToRoleAsync(user!, roleName);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(string? userName, string? email)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(userName))
            {
                var response = new ForgotPasswordResponse
                {
                    Success = false,
                    Message = "Email or Username was left blank."
                };
                return response;
            }
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return new ForgotPasswordResponse
                    {
                        Success = false,
                        Message = "Username nor Email was found in our database."
                    };
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await _emailService.SendResetEmail(user.UserName, code, user.Email);
                return new ForgotPasswordResponse { Success = true, Message = "Email has been sent to reset your password. Please check your email for a code." };
            }

            if (!string.IsNullOrEmpty(userName)) 
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    return new ForgotPasswordResponse
                    {
                        Success = false,
                        Message = "Username nor Email was found in our database."
                    };
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await _emailService.SendResetEmail(user.UserName, code, user.Email);
                return new ForgotPasswordResponse { Success = true, Message = "Email has been sent to reset your password. Please check your email for a code." };
            }

            return new ForgotPasswordResponse { Success = false, Message = "Unknown Error Occurred." };
        }

        public async Task<IList<string>> GetRole(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsUserInRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return await _userManager.IsInRoleAsync(user!, roleName);
        }

        public async Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request)
        {
            var newUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
            };
            var result = _userManager.CreateAsync(newUser, request.Password);

            if (result.Result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.Username);


                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user!);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await _emailService.SendConfirmation(user!.UserName, code, user.Email);

                var registrationSuccess = new AccountRegistrationResponse
                {
                    Success = true
                };

                return registrationSuccess;
            }
            else
            {
                var errors = result.Result.Errors.Select(e => e.Description).ToList();
                var registrationFailure = new AccountRegistrationResponse
                {
                    Success = false,
                    Errors = errors
                };
                return registrationFailure;
            }
        }

        public async Task RemoveRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            await _userManager.RemoveFromRoleAsync(user!, roleName);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return new ResetPasswordResponse { Success = false };

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var result = await _userManager.ResetPasswordAsync(user, code, request.Password);

            if (result.Succeeded)
                return new ResetPasswordResponse { Success = true };

            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new ResetPasswordResponse { Success = false, Errors = errors };
            }

        }
    }
}
