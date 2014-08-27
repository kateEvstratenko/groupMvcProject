using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WishList.ViewModels
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int GiftId { get; set; }

        public int VoutesCount { get; set; }
        public int WishListId { get; set; }

    }
}