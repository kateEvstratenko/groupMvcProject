using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Role: Identity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
