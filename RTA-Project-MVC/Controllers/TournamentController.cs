using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RTA_Project_BL.Models;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fluentx.Mvc;

namespace RTA_Project_MVC.Controllers
{
    public class TournamentController : Controller
    {

        private readonly ITournamentService _tournamentService;
        private readonly IPlayerService _playerService;

        private readonly IMapper _mapper;

        public TournamentController(ITournamentService tournamentService, IPlayerService playerService, IMapper mapper)
        {
            _tournamentService = tournamentService;
            _playerService = playerService;
            _mapper = mapper;
        }


        // GET: Tournament
        public ActionResult Index()
        {
            var tournamentsBL = _tournamentService.QueryTournaments(null, t => !t.IsFinished).ToList();

            var tournaments = _mapper.Map<IEnumerable<TournamentPreviewModel>>(tournamentsBL);

            return View(tournaments);
        }

        // GET: Tournament/Details/5
        public ActionResult Details(int id)
        {
            var tournamentBL = _tournamentService.GetById(id);

            var tournament = _mapper.Map<TournamentDetailsModel>(tournamentBL);

            return View(tournament);
        }

        // GET: Tournament/Create
        [Authorize(Roles = "Host")]
        public ActionResult Create()
        {

            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = Request.GetOwinContext().Get<ApplicationRoleManager>();

            var role = roleManager.Roles.Single(r => r.Name == "Host");
            var hosts = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).Select(x => new SelectListItem
            {
                Text = x.UserName,
                Value = x.Id
            }).ToList();

            var userId = User.Identity.GetUserId();
            hosts.Remove(hosts.First(x => x.Value == userId));

            var model = new TournamentCreateModel() { Year = DateTime.Now.Year, HostsAvailable = hosts };
            return View(model);
        }

        // POST: Tournament/Create
        [HttpPost]
        [Authorize(Roles = "Host")]
        public ActionResult Create(TournamentCreateModel model, params string[] selectedHost)
        {
            if (ModelState.IsValid)
            {
                var modelBL = _mapper.Map<TournamentBL>(model);

                if (selectedHost == null)
                    selectedHost = new string[] { };

                selectedHost.Prepend(User.Identity.GetUserId());
                modelBL.HostsId = string.Join(";", selectedHost);
                modelBL.DateCreated = DateTime.Now;

                _tournamentService.Create(modelBL);
            }

            return RedirectToAction("Index");
        }

        // GET: Tournament/Edit/5
        [Authorize(Roles = "Host")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tournament/Edit/5
        [HttpPost]
        [Authorize(Roles = "Host")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tournament/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tournament/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: Tournament/Register/5
        [HttpPost]
        public ActionResult Register(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }

            var userId = User.Identity.GetUserId();
            var player = _playerService.GetByAccountId(userId);

            if (player == null)
            {
                return RedirectToAction("Register", "Profile");
            }

            _tournamentService.RegisterPlayer(id, player.Id);

            return RedirectToAction("Index");
        }

    }
}
