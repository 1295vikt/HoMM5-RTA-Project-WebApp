﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public string Author { get; set; }
        public DateTime Date { get; set; }

        public bool IsGuideMaterial { get; set; }
    }
}
