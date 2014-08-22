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
            var items = new List<SelectListItem>();
            foreach (var role in roles)
            {
                if (role.Id == roleId)
                {
                    items.Add(new SelectListItem {Text = role.Name, Value = role.Id.ToString(), Selected = true});
                }
                else
                {
                    items.Add(new SelectListItem { Text = role.Name, Value = role.Id.ToString() });
                }
            }
            return items;
        }

        public void SwitchRole(DomainUser user)
        {
            throw new NotImplementedException();
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
