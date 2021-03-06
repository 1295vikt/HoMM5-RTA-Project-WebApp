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

        // GET: Profile/?name=
        [AllowAnonymous]
        public ActionResult Index(string name)
        {
            PlayerBL playerBL;
            if (name == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    playerBL = _playerService.GetByAccountId(userId);
                }
                else
                {
                    return RedirectToAction("LogIn", "Account");
                }
            }
            else
            {
                playerBL = _playerService.GetByName(name);
            }

            if (playerBL != null)
            {


                var model = _mapper.Map<PlayerProfileViewModel>(playerBL);
                               

                return View(model);
            }

            return RedirectToAction("Register");
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
                    var player = new PlayerBL
                    {
                        Name = model.Nickname,
                        AccountId = userId,
                        GuidKey = key,

                        Stats = new PlayerStatsBL
                        {
                            RatingClass = "B",
                            RatingPointsCurrent = 1200,
                            RatingPointsMax = 1200
                        }
                    };

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



        public JsonResult CheckNameIsTaken(string Nickname)
        {
            return Json(!_playerService.CheckIfNameExists(Nickname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckKeyIsInvalid(string GuidKey)
        {
            return Json(_playerService.CheckIfKeyExists(GuidKey), JsonRequestBehavior.AllowGet);
        }

    }
}
