using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using System;
using System.Linq;

namespace RTA_Project_BL.Configs
{
    public class BLAutomapperProfile : Profile
    {
        public BLAutomapperProfile()
        {

            CreateMap<ArticleBL, Article>().ReverseMap();

            CreateMap<TournamentBL, Tournament>().ReverseMap();
            CreateMap<TournamentGroupBL, TournamentGroup>().ReverseMap();
            CreateMap<TournamentDescriptionBL, TournamentDescription>().ReverseMap();
            CreateMap<TournamentPlayerBL, TournamentPlayer>().ReverseMap();
            CreateMap<TournamentGroupPlayerBL, TournamentGroupPlayer>().ReverseMap();
            CreateMap<TournamentBracketBL, TournamentBracket>().ReverseMap();

            CreateMap<MatchBL, Match>();
            CreateMap<Match, MatchBL>().
                ForMember(dest => dest.Player1Name, opt => opt.MapFrom(src => src.Player1.Name)).
                ForMember(dest => dest.Player2Name, opt => opt.MapFrom(src => src.Player2.Name));

            CreateMap<GameBL, Game>().ReverseMap();

            CreateMap<HeroBL, Hero>().ReverseMap();

            CreateMap<PlayerBL, Player>().ReverseMap();
            CreateMap<PlayerStatsBL, PlayerStats>().ReverseMap();

            CreateMap<MapBL, Map>().ReverseMap();

        }

    }
}
