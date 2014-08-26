using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using Ninject.Activation;

namespace WishList.ViewModels
{
    public class CreateGiftViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }

        public string Logo { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string About { get; set; }
    }
}