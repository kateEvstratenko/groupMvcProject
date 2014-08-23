using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WishList.ViewModels
{
    public class WishListDropDownViewModel
    {
       // public int Id { get; set; }
        public string WishListId { get; set; }
        public int GiftId { get; set; }

        public SelectList DropDownList { get; set; }
    }
}