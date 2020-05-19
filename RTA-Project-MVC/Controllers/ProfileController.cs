using AutoMapper;
using Microsoft.AspNet.Identity;
using RTA_Project_BL.Services;
using RTA_Project_MVC.Models;
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

            return RedirectToAction("Details", userId);
        }

        // GET: Profile/Details/5
        public ActionResult Details(string userId)
        {

            var playerProfileBL = _playerService.GetPlayer(userId);

            if (playerProfileBL == null)
            {
                return RedirectToAction("Create");
            }

            var playerProfile = _mapper.Map<PlayerProfileViewModel>(playerProfileBL);

            return View(playerProfile);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(PlayerProfileCreateModel model)
        {




            return RedirectToAction("Index");

        }

        // POST: Profile/Link
        [HttpPost]
        public ActionResult Link(PlayerProfileCreateModel model)
        {


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
    }
}
