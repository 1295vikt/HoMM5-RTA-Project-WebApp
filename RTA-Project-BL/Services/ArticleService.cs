using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System.Linq;

namespace RTA_Project_BL.Services
{
    public interface IArticleService : IGenereicService<ArticleBL>
    {
        IQueryable<ArticleBL> QueryArticles(byte lang);
        ArticleBL GetById(int id);
    }

    public class ArticleService : GenericService<ArticleBL, Article>, IArticleService
    {

        public ArticleService(IGenericRepository<Article> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public ArticleBL GetById(int id)
        {
            var article = _repository.FindById(id);
            var articleBL = Map(article);
            return articleBL;
        }

        public IQueryable<ArticleBL> QueryArticles(byte lang)
        {
            var articles = _repository.Query(x => x.LangId == lang, q => q.OrderByDescending(s => s.Date));
            return Project(articles);
        }
    }
}