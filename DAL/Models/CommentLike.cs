using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CommentLike: Identity
    {
        public int UserId { get; set; }

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
