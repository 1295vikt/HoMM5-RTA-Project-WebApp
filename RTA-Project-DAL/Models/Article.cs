using System;

namespace RTA_Project_DAL.Models
{
    public class Article
    {
        public int Id { get; set; }
        public byte LangId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public DateTime Date { get; set; }
    }
}
