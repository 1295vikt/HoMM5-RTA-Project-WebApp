using RTA_Project_BL.Models;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Helpers;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace RTA_Project_MVC.Controllers
{
    public class StatisticsController : Controller
    {

        private readonly ITournamentService _tournamentService;
        private readonly IHeroService _heroService;

        private readonly IStatsHelper _statsHelper;

        public StatisticsController(ITournamentService tournamentService, IHeroService heroService, IStatsHelper statsHelper)
        {
            _tournamentService = tournamentService;
            _heroService = heroService;

            _statsHelper = statsHelper;
        }

        // GET: Statistics
        public ActionResult Index()
        {

            return View(new StatisticsFilterModel());
        }


        public ActionResult GetStatistics(StatisticsFilterModel model)
        {
            var filtersForGames = new List<Expression<Func<GameBL, bool>>>();
            if (model.FilterByDate)
            {
                Expression<Func<GameBL, bool>> dateFilter = g => g.DateSubmitted >= model.DateFrom && g.DateSubmitted <= model.DateTo;
                filtersForGames.Add(dateFilter);
            }

            if (!model.GetForSingleFaction)
            {
                var games = _tournamentService.GetTournamentGames(null, filtersForGames.ToArray());

                if (model.FilterByDate)
                    games = games.Where(g => g.DateSubmitted >= model.DateFrom && g.DateSubmitted <= model.DateTo);

                var statsModel = _statsHelper.RecoverAllFactionsStats(games.ToList());

                return PartialView("_AllFactionsStatsPartial", statsModel);
            }
            else
            {
                var factionId = model.FactionId;

                var heroes = _heroService.GetAllByFaction(model.FactionId);
                var heroesId = heroes.Select(h => h.Id).ToArray();

                Expression<Func<GameBL, bool>> factionFilter = g => g.Faction1Id == factionId || g.Faction2Id == factionId;
                filtersForGames.Add(factionFilter);

                var games = _tournamentService.GetTournamentGames(null, filtersForGames.ToArray());


                var statsModel = _statsHelper.RecoverFactionHeroesStats(games.ToList(), factionId, heroesId);
                var heroNames = heroes.Select(h => h.NameRus);
                statsModel.HeroNames = heroNames.ToArray();

                return PartialView("_FactionHeroesStatsPartial", statsModel);
            }

        }



    }
}
