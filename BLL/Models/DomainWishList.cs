using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainWishList: DomainIdentity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }
        public virtual DomainUser User { get; set; }
        public ICollection<DomainGift> Gifts { get; set; }
        public ICollection<DomainView> Views { get; set; }
        public ICollection<DomainVote> Votes { get; set; }
        public ICollection<DomainComment> Comments { get; set; }
    }
}
