using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int WishListId { get; set; }

        public int GiftId { get; set; }

        public virtual User User { get; set; }
        public virtual WishList WishList { get; set; }
        public virtual Gift Gift { get; set; }
    }
}
