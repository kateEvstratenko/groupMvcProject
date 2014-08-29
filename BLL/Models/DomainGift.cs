using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class DomainGift: DomainIdentity
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string About { get; set; }

        public int LikesCount { get; set; }

        public int ViewsCount { get; set; }

        public int WishListId { get; set; }

        public DomainWishList WishList { get; set; }
        public ICollection<DomainComment> Comments { get; set; }
    }
}
