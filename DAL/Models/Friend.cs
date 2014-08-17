using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Friend: Identity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
