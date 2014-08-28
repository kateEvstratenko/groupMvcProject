using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Models;

namespace WishList.ViewModels
{
    public class CreateWishListViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }
        public IEnumerable<string> FriendsId { get; set; }
        public MultiSelectList FriendsList { get; set; }
        public IEnumerable<DomainFriend> Friends { get; set; }
    }
}