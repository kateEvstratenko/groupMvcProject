using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class View: Identity
    {
        public int UserId { get; set; }

        public int GiftId { get; set; }

    }
}
