using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTA_Project_MVC.App_Start
{
    public class WebAutomapperProfile : Profile
    {
        public WebAutomapperProfile()
        {
            CreateMap<ArticleViewModel, ArticleBL>().ReverseMap();
        }

    }
}