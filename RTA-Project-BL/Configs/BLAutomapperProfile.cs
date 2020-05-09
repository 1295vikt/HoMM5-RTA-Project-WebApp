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
            CreateMap<TournamentGroupBL, TournamentGroup>().ReverseMap();
            CreateMap<TournamentDescriptionBL, TournamentDescription>().ReverseMap();
            CreateMap<TournamentPlayerBL, TournamentPlayer>().ReverseMap();
            CreateMap<TournamentBracketBL, TournamentBracket>().ReverseMap();
            CreateMap<MatchBL, Match>().ReverseMap();
            CreateMap<GameBL, Game>().ReverseMap();
            CreateMap<HeroBL, Hero>().ReverseMap();
            CreateMap<PlayerBL, Player>().ReverseMap();
            CreateMap<PlayerStatsBL, PlayerStats>().ReverseMap();

        }

    }
}
