using RTA_Project_BL.Services;
using RTA_Project_MVC.Helpers;
using System.Linq;
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



            //var heroes = _heroService.GetAllByFaction(1);

            //var heroesId = heroes.Select(h => h.Id).ToArray();

            //var games = _tournamentService.QueryTournamentGames(null, g => g.Faction1Id == 1 || g.Faction2Id == 1);

            //var g2 = games.ToList();

            //var f1 = _statsHelper.MapToFactionStats(g2, 1);

            //var f2 = _statsHelper.MapToFactionStats(g2, 1, heroesId);

            return View();
        }


    }
}
