using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BLL.Services
{
    public class UserService: BaseService, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {
            Uow.UserManager = InitUserManager(Uow.UserManager);
        }

        private UserManager<User, int> InitUserManager(UserManager<User, int> manager)
        {
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Wishlist");
            manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(provider.Create("EmailConfirmation"));
            return manager;
        }

        public async Task<DomainUser> FindAsync(string userName, string password)
        {
            var user = await Uow.UserManager.FindAsync(userName, password);
            var applicationUserDomainModel = Mapper.Map<DomainUser>(user);
            return applicationUserDomainModel;
        }

        public DomainUser Register(DomainUser model, string password, IAuthenticationManager authenticationManager)
        {
            var user = Mapper.Map<User>(model);
            var result = Uow.UserManager.Create(user, password);
            if (!result.Succeeded)
            {
                return null;
            }
            Uow.UserManager.AddToRole(user.Id, "User");
            return Mapper.Map<DomainUser>(user);
        }

        public async Task<String> GenerateEmailConfirmationTokenAsync(int id)
        {
            string code = await Uow.UserManager.GenerateEmailConfirmationTokenAsync(id);
            return code;
        }

        public async Task SendEmailAsync(int userId, string message, string body)
        {
            await Uow.UserManager.SendEmailAsync(userId, message, body);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(int userId, string code)
        {
            IdentityResult result = await Uow.UserManager.ConfirmEmailAsync(userId, code);
            return result;
        }

        public async Task<bool> IsEmailConfirmedAsync(int userId)
        {
            bool isEmailConfirmed = await Uow.UserManager.IsEmailConfirmedAsync(userId);

            return isEmailConfirmed;
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

        public async Task<ClaimsIdentity> GenerateClaimAsync(DomainUser userDomainModel)
        {
            var user = Mapper.Map<User>(userDomainModel);
            var claimsIdentity = await Uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claimsIdentity;
        }

        public async Task<IdentityResult> UpdateUserAsync(int userId, DomainUser model)
        {
            var user = Uow.UserManager.FindById(userId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Birthday = model.Birthday;
            var result = await Uow.UserManager.UpdateAsync(user);
            return result;
        }

        public void SignOut(IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut();
        }

        public IQueryable<DomainUser> GetAll()
        {
            var users = Uow.UserRepository.GetAll();
            var domainUsers = users.Select(Mapper.Map<User, DomainUser>);
            return domainUsers.OrderBy(u => u.UserName).AsQueryable();
        }


    }
}
