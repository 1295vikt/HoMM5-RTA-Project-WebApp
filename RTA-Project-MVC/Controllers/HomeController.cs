using AutoMapper;
using System.Linq;
using RTA_Project_BL.Services;
using RTA_Project_DAL.enums;
using System.Web.Mvc;
using System.Collections.Generic;
using RTA_Project_MVC.Models;

namespace RTA_Project_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IArticleService _articleService;

        private readonly IMapper _mapper;

        public HomeController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;

            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var newsBL = _articleService.QueryArticles((byte)Lang.Rus).Take(3);

            var news = _mapper.Map<IEnumerable<ArticleViewModel>>(newsBL);

            return View(news);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}