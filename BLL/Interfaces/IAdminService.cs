using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Models;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        IQueryable<DomainUser> GetUsers();
        List<SelectListItem> GetRoles(int roleId);
        void SwitchRole(DomainUser user);
        void DeleteUser(DomainUser user);
        void DeleteWishlist(DomainWishList wishlist);
    }
}
