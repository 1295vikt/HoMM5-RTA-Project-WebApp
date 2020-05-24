using AutoMapper;
using RTA_Project_BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTA_Project_MVC.Controllers
{
    public class RTAController : Controller
    {

        private readonly IMapService _mapService;

        private readonly IMapper _mapper;

        public RTAController(IMapService mapService, IMapper mapper)
        {
            _mapService = mapService;

            _mapper = mapper;
        }

        // GET: RTA
        public ActionResult Index()
        {
            return View();
        }

        // GET: RTA/Details/?version=
        public ActionResult Details(string version)
        {
            return View();
        }

        // GET: RTA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RTA/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RTA/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RTA/Edit/5
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

        // GET: RTA/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RTA/Delete/5
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


        public JsonResult CheckIfVersionExists(string MapVersion)
        {
            return Json(_mapService.QueryAll().Any(m => m.Version == MapVersion), JsonRequestBehavior.AllowGet);
        }

    }
}
