using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTA_Project_MVC.Models
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}