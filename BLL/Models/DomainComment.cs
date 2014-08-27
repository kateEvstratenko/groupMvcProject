using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class DomainComment:DomainIdentity
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Message { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public int? WishListId { get; set; }

        public int? GiftId { get; set; }

        public DomainUser User { get; set; }
        public DomainWishList WishList { get; set; }
        public DomainGift Gift { get; set; }
    }
}
