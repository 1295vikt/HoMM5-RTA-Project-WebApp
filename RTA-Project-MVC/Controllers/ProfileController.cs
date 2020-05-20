﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using RTA_Project_BL.Models;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RTA_Project_MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        private readonly IPlayerService _playerService;

        private readonly IMapper _mapper;

        public ProfileController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;

            _mapper = mapper;
        }

        // GET: Profile
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            return RedirectToAction("Details", "Profile", userId);
        }

        // GET: Profile/Details/5
        public ActionResult Details(string userId)
        {

            var playerProfileBL = _playerService.GetPlayer(userId);

            if (playerProfileBL == null)
            {
                return RedirectToAction("Register");
            }

            var playerProfile = _mapper.Map<PlayerProfileViewModel>(playerProfileBL);

            return View(playerProfile);
        }


        // GET: Profile/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(PlayerProfileCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                if (!_playerService.QueryAll().Any(p=>p.AccountId==userId))
                {
                    var key = Guid.NewGuid().ToString();
                    var player = new PlayerBL { Name = model.Nickname, AccountId = userId, GuidKey = key };

                    _playerService.Create(player);
                }
            }

            return RedirectToAction("Index");

        }

        // POST: Profile/Link
        [HttpPost]
        public ActionResult Link(PlayerProfileLinkModel model)
        {
            if (ModelState.IsValid)
            {
                var accountId = User.Identity.GetUserId();
                _playerService.LinkAccountToPlayer(accountId, model.GuidKey);
            }

            return RedirectToAction("Index");
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Edit/5
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

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
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

        public JsonResult CheckIfNameExists(string Nickname)
        {
            return Json(!_playerService.QueryAll().Any(p => p.Name == Nickname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfKeyExists(string GuidKey)
        {
            return Json(_playerService.QueryAll().Any(p => p.GuidKey == GuidKey), JsonRequestBehavior.AllowGet);
        }

    }
}
