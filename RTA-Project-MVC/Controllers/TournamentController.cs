using AutoMapper;
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
        private readonly ITournamentGroupService _tournamentGroupService;
        private readonly IPlayerService _playerService;

        private readonly IMapper _mapper;

        public TournamentController(ITournamentService tournamentService, ITournamentGroupService tournamentGroupService, IPlayerService playerService, IMapper mapper)
        {
            _tournamentService = tournamentService;
            _tournamentGroupService = tournamentGroupService;
            _playerService = playerService;
            _mapper = mapper;
        }

        // GET: Tournament
        public ActionResult Index()
        {
            var s = User.Identity;
            return View();
        }

        // GET: Tournament/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tournament/Create
        [Authorize(Roles = "Host")]
        public ActionResult Create()
        {

            var players = _playerService.QueryAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var model = new TournamentCreateModel() { Year = DateTime.Now.Year, HostPlayerList = players.ToList()};
            return View(model);
        }

        // POST: Tournament/Create
        [HttpPost]
        public ActionResult Create(TournamentCreateModel model)
        {
            var m = model;
            return View();
        }

        // GET: Tournament/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tournament/Edit/5
        [HttpPost]
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
    }
}
