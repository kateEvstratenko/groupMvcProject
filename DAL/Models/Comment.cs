using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DAL.Models
{
    public class Comment:Identity
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Message { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public int? WishListId { get; set; }

        public int? GiftId { get; set; }

        public virtual User User { get; set; }
        public virtual WishList WishList { get; set; }
        public virtual Gift Gift { get; set; }
        public virtual IQueryable<CommentLike> CommentLike { get; set; } 
    }
}
