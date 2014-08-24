using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Like : Identity
    {
        public int GiftId { get; set; }
        public int UserId { get; set; }
    }
}
