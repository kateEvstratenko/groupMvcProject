using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<DomainUser> LoginAsync(string userName, string password, bool isPersistent, IAuthenticationManager authenticationManager);

        Task<IdentityResult> RegisterAsync(string userName, string password, IAuthenticationManager authenticationManager);

        Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        DomainUser GetUser(int userId);

        Task<IdentityResult> UpdateUserAsync(int userId, DomainUser model);

        void SignOut(IAuthenticationManager authenticationManager);
    }
}
