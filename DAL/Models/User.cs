using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User: Identity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
