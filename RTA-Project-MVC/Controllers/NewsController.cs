﻿using AutoMapper;
using PagedList;
using RTA_Project_BL.Services;
using RTA_Project_DAL.enums;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTA_Project_MVC.Controllers
{
    public class NewsController : Controller
    {

        private readonly IArticleService _articleService;

        private readonly IMapper _mapper;

        public NewsController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        // GET: News
        public ActionResult Index(int? page)
        {

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var articlesBL = _articleService.QueryArticles((byte)Lang.Rus);
            var articlesPageBL = articlesBL.ToPagedList(pageNumber, pageSize);

            var mappedList = _mapper.Map<IEnumerable<ArticleViewModel>>(articlesPageBL);
            var pagedResult = new StaticPagedList<ArticleViewModel>(mappedList, articlesPageBL.GetMetaData());

            return View(pagedResult);
        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
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

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: News/Edit/5
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

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: News/Delete/5
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