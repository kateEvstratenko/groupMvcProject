using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(IUnitOfWork uow) : base(uow) { }

        public IQueryable<DomainUser> GetUsers()
        {
            var users = Uow.UserRepository.GetAll();
            var domainUsers = users.Select(Mapper.Map<User, DomainUser>);
            return domainUsers.AsQueryable();
        }

        public List<SelectListItem> GetRoles(int roleId)
        {
            var roles = Uow.RolesManager.Roles.Take(3).AsEnumerable();
            return roles.Select(role => role.Id == roleId ? new SelectListItem {Text = role.Name, Value = role.Id.ToString(), Selected = true} : new SelectListItem {Text = role.Name, Value = role.Id.ToString()}).ToList();
        }

        public void SwitchRole(int userId, int roleId)
        {
            var user = Uow.UserRepository.Get(userId);
            var f = Uow.UserManager.GetRoles(userId);
            Uow.UserManager.RemoveFromRole(userId, f[0]);
            var role = Uow.RolesManager.FindById(roleId);
            Uow.UserManager.AddToRole(userId, role.Name);
        }

        public void DeleteUser(DomainUser user)
        {
            throw new NotImplementedException();
        }

        public void DeleteWishlist(DomainWishList wishlist)
        {
            throw new NotImplementedException();
        }
    }
}
