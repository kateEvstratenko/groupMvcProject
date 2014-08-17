using DAL.Models;

namespace BLL.Models
{
    public class DomainFriend: DomainIdentity
    {
        public int UserId { get; set; }

        public DomainUser User { get; set; }
    }
}
