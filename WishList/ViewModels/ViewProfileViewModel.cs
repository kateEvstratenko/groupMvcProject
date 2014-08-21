using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WishList.ViewModels
{
    public class ViewProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string LastName { get; set; }

        [Display(Name = "Birthday")]
        public string FormattedBirthday { get; set; }

        public string Avatar { get; set; }
    }
}