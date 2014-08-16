using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IGiftService
    {
        void Create(DomainGift gift);
        void Delete(int id);
    }
}
