using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainRole: DomainIdentity
    {
        public string Name { get; set; }
        public virtual ICollection<DomainUser> Users { get; set; }
    }
}
