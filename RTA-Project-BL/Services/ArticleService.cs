using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace RTA_Project_BL.Services
{
    public interface IArticleService : IGenereicService<ArticleBL>
    {
    }

    public class ArticleService : GenericService<ArticleBL, Article>, IArticleService
    {

        public ArticleService(IGenericRepository<Article> repository, IMapper mapper) : base(repository, mapper)
        {

        }


    }
}