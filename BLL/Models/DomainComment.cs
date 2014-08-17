using System;
using DAL.Models;

namespace BLL.Models
{
    public class DomainComment:DomainIdentity
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int WishListId { get; set; }
        public int GiftId { get; set; }
        public virtual DomainUser User { get; set; }
        public virtual DomainWishList WishList { get; set; }
        public virtual DomainGift Gift { get; set; }
    }
}
