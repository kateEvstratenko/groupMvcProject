using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Comment:Identity
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int WishListId { get; set; }
        public int GiftId { get; set; }
        public virtual User User { get; set; }
        public virtual WishList WishList { get; set; }
        public virtual Gift Gift { get; set; }
    }
}
