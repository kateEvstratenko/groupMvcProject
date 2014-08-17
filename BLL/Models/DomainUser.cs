using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class DomainUser: DomainIdentity
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Avatar { get; set; }

        public ICollection<DomainRole> Roles { get; set; }
        public ICollection<DomainFriend> Friends { get; set; }
        public ICollection<DomainWishList> WishLists { get; set; }
        public ICollection<DomainComment> Comments { get; set; }
    }
}
