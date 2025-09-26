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
        Task AssignRole(string userName, string roleName);
        Task RemoveRole(string userName, string roleName);
        Task<bool> IsUserInRole(string userName, string roleName);
    }
}
