using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using RTA_Project_BL.Models;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var tournamentsBL = _tournamentService.QueryAll().OrderByDescending(t => t.DateCreated);
            var tournamentsPageBL = tournamentsBL.ToPagedList(pageNumber, pageSize);

            var mappedList = _mapper.Map<IEnumerable<TournamentPreviewModel>>(tournamentsPageBL);

            var pagedResult = new StaticPagedList<TournamentPreviewModel>(mappedList, tournamentsPageBL.GetMetaData());

            return View(pagedResult);
        }

        // GET: Tournament/Details/5
        public ActionResult Details(int id)
        {
            var tournamentBL = _tournamentService.GetById(id);

            var tournament = _mapper.Map<TournamentDetailsModel>(tournamentBL);

            var hosts = new List<string>();
            if (tournamentBL.HostsId != null)
            {
                var hostsId = tournamentBL.HostsId.Split(';');

                var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();               
                foreach (var hostId in hostsId)
                {
                    var host = userManager.Users.FirstOrDefault(u => u.Id == hostId);
                    if (host != null)
                        hosts.Add(host.UserName);
                }               
            }
            tournament.TournamentHosts = hosts;

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var modelBL = _mapper.Map<TournamentBL>(model);

            if (selectedHost == null)
                selectedHost = new string[] { };

            var userId = User.Identity.GetUserId();
            modelBL.HostsId = $"{userId};{string.Join(";", selectedHost)}";
            modelBL.DateCreated = DateTime.Now;

            _tournamentService.Create(modelBL);

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
