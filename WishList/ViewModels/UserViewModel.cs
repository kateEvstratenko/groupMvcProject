using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
        public DateTime Birthday { get; set; }

        [Required]
        public string Avatar { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<DAL.Models.WishList> WishLists { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}