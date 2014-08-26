
namespace BLL.Models
{
    public class DomainVote: DomainIdentity
    {
        public int UserId { get; set; }

        public int GiftId { get; set; }

        public int WishListId { get; set; }

        public DomainWishList WishList { get; set; }
    }
}
