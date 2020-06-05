using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_MVC.Models;
using System.Linq;

namespace RTA_Project_MVC.App_Start
{
    public class WebAutomapperProfile : Profile
    {
        public WebAutomapperProfile()
        {
            CreateMap<PlayerBL, PlayerProfileViewModel>().AfterMap((src, dest, context) => context.Mapper.Map(src.Stats, dest));
            CreateMap<PlayerStatsBL, PlayerProfileViewModel>();

            CreateMap<ArticleViewModel, ArticleBL>().ReverseMap();

            CreateMap<TournamentCreateModel, TournamentBL>().ForPath(dest => dest.Description.ContentRus, opt => opt.MapFrom(src => src.ContentRus)).ReverseMap();

            CreateMap<TournamentBL, TournamentPreviewModel>();

            CreateMap<TournamentBL, TournamentDetailsModel>().ForMember(dest => dest.TournamentPlayers,
                opt => opt.MapFrom(src => src.TournamentPlayers.Select(tp => tp.Player.Name))).
                ForMember(dest => dest.Content, opt => opt.MapFrom(src => src==null ? "" : src.Description.ContentRus)).ReverseMap();

            // TODO: change content to support ENG


        }

    }
}