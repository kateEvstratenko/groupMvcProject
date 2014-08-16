using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models;
using DAL.Models;

namespace BLL.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            ConfigureGiftMapping();
        }

        private static void ConfigureGiftMapping()
        {
            Mapper.CreateMap<DomainGift, Gift>();
            Mapper.CreateMap<Gift, DomainGift>();
        }
    }
}
