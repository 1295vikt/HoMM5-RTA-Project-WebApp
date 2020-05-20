using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using RTA_Project_BL.Models;
using RTA_Project_BL.Services;
using RTA_Project_DAL.enums;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
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
        [Authorize(Roles = "Host")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [Authorize(Roles = "Host")]
        public ActionResult Create(ArticleViewModel article)
        {

            if (!ModelState.IsValid)
            {
                return View(article);
            }

            article.LangId = Lang.Rus;

            var articleBL = _mapper.Map<ArticleBL>(article);

            articleBL.AuthorId = User.Identity.GetUserId();
            articleBL.Date = DateTime.Now;

            _articleService.Create(articleBL);

            return RedirectToAction("Index");
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


        // POST: News/Delete/5
        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id)
        {

            var userId = User.Identity.GetUserId();

            var authorId = _articleService.FindById(id).AuthorId;

            if (authorId == userId)
                _articleService.Delete(id);

            return View();
        }
    }
}
