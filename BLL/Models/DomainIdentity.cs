using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public abstract class DomainIdentity
    {
        [Required]
        public int Id { get; set; }
    }
}
