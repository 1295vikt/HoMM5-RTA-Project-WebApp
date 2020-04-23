using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Configs
{
    public class BLAutomapperProfile : Profile
    {
        public BLAutomapperProfile()
        {
            CreateMap<TournamentBL, Tournament>().ReverseMap();
            CreateMap<PlayerBL, Player>().ReverseMap();

        }

    }
}
