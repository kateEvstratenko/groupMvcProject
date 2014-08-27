using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DAL.Models;

namespace WishList.ViewModels
{
    public class CommentViewModel
    {
        public int Id;
        [Required]
        [StringLength(512, ErrorMessage = "The {0} must be maximum {1} characters long.")]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public int GiftId { get; set; }
        public int WishListId { get; set; }
    }
}