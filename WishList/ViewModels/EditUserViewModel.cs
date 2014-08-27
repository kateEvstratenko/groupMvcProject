using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WishList.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Display(Name = "Birthday")]
        public string FormattedBirthday { get; set; }

        public string Avatar { get; set; }
    }
}