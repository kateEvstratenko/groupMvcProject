using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WishList.ViewModels
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }

        public string Link { get; set; }

        public int UserId { get; set; }

        public ICollection<GiftViewModel> Gifts { get; set; }
    }
}