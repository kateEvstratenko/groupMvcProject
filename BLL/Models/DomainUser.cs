using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainUser: DomainIdentity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; }
        public virtual ICollection<DomainRole> Roles { get; set; }
        public virtual ICollection<DomainFriend> Friends { get; set; }
        public virtual ICollection<DomainWishList> WishLists { get; set; }
        public virtual ICollection<DomainComment> Comments { get; set; }
    }
}
