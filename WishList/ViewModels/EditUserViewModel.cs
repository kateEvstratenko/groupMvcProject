using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WishList.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [Display(Name = "Firstame")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name = "Birthday")]
        public string FormattedBirthday { get; set; }
    }
}