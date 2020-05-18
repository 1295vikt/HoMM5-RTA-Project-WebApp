﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public TournamentController(ITournamentService tournamentService, ITournamentGroupService tournamentGroupService,
            IPlayerService playerService, IMapper mapper)
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


            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = Request.GetOwinContext().Get<ApplicationRoleManager>();

            var role =  roleManager.Roles.Single(r => r.Name == "Host");

            var hosts = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).Select(x => new SelectListItem
            {
                Text = x.UserName,
                Value = x.Id
            }).ToList();

            var userId = User.Identity.GetUserId();

            var model = new TournamentCreateModel() { Year = DateTime.Now.Year, HostsIdList = new List<string> { userId }, HostsAvailable = hosts };
            return View(model);
        }

        // POST: Tournament/Create
        [HttpPost]
        [Authorize(Roles = "Host")]
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
