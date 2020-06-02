using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using RTA_Project_BL.Configs;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RTA_Project_MVC.App_Start
{
    public class WebAutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var config = new MapperConfiguration(cfg => cfg.AddProfiles(
                  new List<Profile>() { new WebAutomapperProfile(), new BLAutomapperProfile() }));
            builder.Register(c => config.CreateMapper());

            builder.RegisterType<PlayerService>().As<IPlayerService>();
            builder.RegisterType<TournamentService>().As<ITournamentService>();
            builder.RegisterType<MapService>().As<IMapService>();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<HeroService>().As<IHeroService>();

            builder.RegisterType<StatsHelper>().As<IStatsHelper>();

            builder.RegisterModule<BLAutofacConfig>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}