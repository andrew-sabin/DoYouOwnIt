using DoYouOwnIt_Shared.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Account
{
    public interface IAccountService
    {
        Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(string? userName, string? email);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task AssignRole(string userName, string roleName);
        Task RemoveRole(string userName, string roleName);
        Task<IList<string>> GetRole(string userName);
        Task<bool> IsUserInRole(string userName, string roleName);
    }
}
