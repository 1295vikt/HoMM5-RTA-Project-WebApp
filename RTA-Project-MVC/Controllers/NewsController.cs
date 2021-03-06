﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using RTA_Project_BL.Models;
using RTA_Project_BL.Services;
using RTA_Project_DAL.enums;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Net;
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

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var articlesBL = _articleService.QueryArticles((int)Lang.Rus);
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
        [Authorize(Roles = "Host")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [Authorize(Roles = "Host")]
        public ActionResult Create(ArticleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.LangId = Lang.Rus;

            var articleBL = _mapper.Map<ArticleBL>(model);

            articleBL.AuthorId = User.Identity.GetUserId();
            articleBL.Date = DateTime.Now;

            _articleService.Create(articleBL);

            return RedirectToAction("Index");
        }

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            var articleBL = _articleService.GetById(id);

            var model = _mapper.Map<ArticleViewModel>(articleBL);

            model.Content = WebUtility.HtmlDecode(model.Content);

            return View(model);
        }

        // POST: News/Edit/5
        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var articleBL = _mapper.Map<ArticleBL>(model);

            _articleService.Update(articleBL);

            return RedirectToAction("Index");
        }


        // POST: News/Delete/5
        [HttpDelete]
        [Authorize(Roles = "Host")]
        public ActionResult Delete(int id)
        {
            _articleService.Delete(id);

            return new EmptyResult();
        }
    }
}
