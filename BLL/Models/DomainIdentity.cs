using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Models
{
    public abstract class DomainIdentity
    {
        [Required]
        public int Id { get; set; }
    }
}
