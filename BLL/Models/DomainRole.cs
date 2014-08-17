using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class DomainRole: DomainIdentity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<DomainUser> Users { get; set; }
    }
}
