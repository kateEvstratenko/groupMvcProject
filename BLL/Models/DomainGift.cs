using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainGift: DomainIdentity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string About { get; set; }
        public int LikesCount { get; set; }
        public int WishListId { get; set; }
        public virtual DomainWishList WishList { get; set; }
        public virtual ICollection<DomainTag> Tags { get; set; }
        public virtual ICollection<DomainComment> Comments { get; set; }
    }
}
