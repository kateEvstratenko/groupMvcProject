using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Friend: Identity
    {
        
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FriendId { get; set; }

        public virtual User User { get; set; }

        public ICollection<WishList> WishLists { get; set; } 
    }
}
