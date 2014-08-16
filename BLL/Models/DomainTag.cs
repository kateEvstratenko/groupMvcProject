using System.Collections.Generic;

namespace BLL.Models
{
    public class DomainTag: DomainIdentity
    {
        public string Name { get; set; }
        public virtual ICollection<DomainGift> Gifts { get; set; }
    }
}
