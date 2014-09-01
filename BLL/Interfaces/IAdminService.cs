using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        IQueryable<DomainUser> GetUsers();
        List<SelectListItem> GetRoles(int roleId);
        void SwitchRole(int userId, int roleId);
        void DeleteUser(int userId);
        void DeleteWishlist(int id);
    }
}
