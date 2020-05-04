﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        public TournamentController(ITournamentService tournamentService, ITournamentGroupService tournamentGroupService, IMapper mapper)
        {
            _tournamentService = tournamentService;
            _tournamentGroupService = tournamentGroupService;
            _mapper = mapper;
        }

        // GET: Tournament
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tournament/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tournament/Create
        public ActionResult Create()
        {
            var crap = _tournamentService.GetAll();
            var model = new TournamentCreateModel() { Year = DateTime.Now.Year};
            return View(model);
        }

        // POST: Tournament/Create
        [HttpPost]
        public ActionResult Create(TournamentCreateModel modlel)
        {
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
