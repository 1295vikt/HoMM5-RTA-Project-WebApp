using RTA_Project_DAL.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTA_Project_MVC.Models
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public Lang LangId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}