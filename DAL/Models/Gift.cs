using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Gift: Identity
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string About { get; set; }

        public int LikesCount { get; set; }

        public int WishListId { get; set; }

        public virtual WishList WishList { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
