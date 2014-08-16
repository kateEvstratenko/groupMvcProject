using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Gift: Identity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string About { get; set; }
        public int LikesCount { get; set; }
        public int WishListId { get; set; }
        public virtual WishList WishList { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
