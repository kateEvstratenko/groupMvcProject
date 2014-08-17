using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WishList: Identity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Gift> Gifts { get; set; }
        public ICollection<View> Views { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
