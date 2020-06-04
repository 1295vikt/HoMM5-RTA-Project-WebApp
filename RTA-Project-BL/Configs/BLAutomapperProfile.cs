using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;

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

            CreateMap<TournamentPlayer, TournamentPlayerBL>().ForPath(dest=>dest.Player.Stats, opt=>opt.Ignore()).ReverseMap();

            CreateMap<TournamentGroupPlayerBL, TournamentGroupPlayer>().ReverseMap();
            CreateMap<TournamentBracketBL, TournamentBracket>().ReverseMap();

            CreateMap<MatchBL, Match>();
            CreateMap<Match, MatchBL>().
                ForMember(dest => dest.Player1Name, opt => opt.MapFrom(src => src.Player1.Name)).
                ForMember(dest => dest.Player2Name, opt => opt.MapFrom(src => src.Player2.Name));

            CreateMap<GameBL, Game>().ReverseMap();

            CreateMap<HeroBL, Hero>().ReverseMap();

            CreateMap<PlayerBL, Player>().ReverseMap();

            CreateMap<PlayerStats, PlayerStatsBL>().ForMember(dest => dest.GamesPlayedFaction, opt => opt.MapFrom(src => new int[]
            {
                src.GamesAsAcademy,
                src.GamesAsDungeon,
                src.GamesAsFortress,
                src.GamesAsHaven,
                src.GamesAsInferno,
                src.GamesAsNecropolis,
                src.GamesAsStronghold,
                src.GamesAsSylvan
            })).
            ForMember(dest => dest.GamesWonFaction, opt => opt.MapFrom(src => new int[]
            {
                src.WinsAsAcademy,
                src.WinsAsDungeon,
                src.WinsAsFortress,
                src.WinsAsHaven,
                src.WinsAsInferno,
                src.WinsAsNecropolis,
                src.WinsAsStronghold,
                src.WinsAsSylvan
            }));

            CreateMap<MapBL, Map>().ReverseMap();

        }

    }
}
