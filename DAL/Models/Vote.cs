using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Vote: Identity
    {
        public int UserId { get; set; }

        public int GiftId { get; set; }

        public int WishListId { get; set; }

        public virtual WishList WishList { get; set; }
    }
}
