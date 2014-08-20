using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace BLL.Services
{
    public class UserService: BaseService, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow) { }

        public async Task<DomainUser> LoginAsync(string userName, string password, bool isPersistent, IAuthenticationManager authenticationManager)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new NoNullAllowedException();
            }

            var user = await Uow.UserManager.FindAsync(userName, password);

            if (user == null) return null;

            await SignInAsync(user, isPersistent, authenticationManager);
            var model = Mapper.Map<DomainUser>(user);

            return model;
        }

        public async Task<IdentityResult> RegisterAsync(DomainUser model, string password, IAuthenticationManager authenticationManager)
        {
            var user = Mapper.Map<User>(model);
            var result = await Uow.UserManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var result = await Uow.UserManager.ChangePasswordAsync(userId, currentPassword, newPassword);

            return result;
        }

        public DomainUser GetUser(int userId)
        {
            var user = Uow.UserManager.FindById(userId);
            if (user == null)
            {
                return null;
            }

            var model = Mapper.Map<DomainUser>(user);

            return model;
        }

        public async Task<IdentityResult> UpdateUserAsync(int userId, DomainUser model)
        {
            var user = Uow.UserManager.FindById(userId);
            user = Mapper.Map<User>(model);
            var result = await Uow.UserManager.UpdateAsync(user);

            return result;
        }

        public void SignOut(IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut();
        }

        private async Task SignInAsync(User user, bool isPersistent, IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await Uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

    }
}
