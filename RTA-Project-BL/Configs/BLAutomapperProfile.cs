using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using System.Linq;

namespace RTA_Project_BL.Configs
{
    public class BLAutomapperProfile : Profile
    {
        public BLAutomapperProfile()
        {

            CreateMap<ArticleBL, Article>().ReverseMap();
            CreateMap<TournamentBL, Tournament>().ForMember(dest => dest.HostsId, opt => opt.MapFrom(src => string.Join(";", src.HostsId)));
            CreateMap<Tournament, TournamentBL>().ForMember(dest => dest.HostsId, opt => opt.MapFrom(src => src.HostsId.Split(';')));
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
