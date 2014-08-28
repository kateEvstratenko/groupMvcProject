using System.Collections.Generic;
using DAL.Models;

namespace BLL.Models
{
    public class DomainFriend: DomainIdentity
    {
        public int FriendId { get; set; }
        public int UserId { get; set; }
        public DomainUser User { get; set; }

        public ICollection<DomainWishList> WishLists { get; set; }
    }
}
