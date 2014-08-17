
namespace DAL.Models
{
    public class Friend: Identity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
