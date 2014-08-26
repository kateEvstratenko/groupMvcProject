using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        DomainUser Register(DomainUser model, string password, IAuthenticationManager authenticationManager);

        Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        DomainUser GetUser(int userId);

        Task<ClaimsIdentity> GenerateClaimAsync(DomainUser userDomainModel);

        Task<DomainUser> FindAsync(string userName, string password);

        Task<String> GenerateEmailConfirmationTokenAsync(int id);

        Task<bool> IsEmailConfirmedAsync(int userId);

        Task SendEmailAsync(int userId, string message, string body);

        Task<IdentityResult> ConfirmEmailAsync(int userId, string code);

        Task<IdentityResult> UpdateUserAsync(int userId, DomainUser model);

        void SignOut(IAuthenticationManager authenticationManager);
    }
}
