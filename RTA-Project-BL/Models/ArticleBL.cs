using System;

namespace RTA_Project_BL.Models
{

    public class ArticleBL
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public DateTime Date { get; set; }
    }
}
