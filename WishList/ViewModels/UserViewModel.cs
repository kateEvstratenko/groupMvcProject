using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;

namespace WishList.ViewModels
{
    public class UserViewModel
    {
        public int Id;
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string LastName { get; set; }

        [Required]
        public string Avatar { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FormattedBirthday { get; set; }

        public List<SelectListItem> Roles { get; set; } 
        
        public int RoleId { get; set; }

        public int NumberOfPages { get; set; }

        public int CurrentPage { get; set; }
    }
}