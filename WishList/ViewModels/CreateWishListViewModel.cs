using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WishList.ViewModels
{
    public class CreateWishListViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }
    }
}