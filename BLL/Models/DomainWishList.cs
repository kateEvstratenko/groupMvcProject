using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class DomainWishList: DomainIdentity
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        public int UserId { get; set; }

        public DomainUser User { get; set; }

        public ICollection<DomainGift> Gifts { get; set; }
        public ICollection<DomainFriend> Friends { get; set; }
        public ICollection<DomainView> Views { get; set; }
        public ICollection<DomainVote> Votes { get; set; }
        public ICollection<DomainComment> Comments { get; set; }
    }
}
