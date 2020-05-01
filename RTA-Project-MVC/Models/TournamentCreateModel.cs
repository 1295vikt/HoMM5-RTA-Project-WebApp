using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class TournamentCreateModel
    {
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        public string MapVersion { get; set; }
        public int Year { get; set; }
        public byte Season { get; set; }

        public bool Is2Stage { get; set; }

        public byte Stage1Format { get; set; }
        public byte Stage2Format { get; set; }

        public bool IsOfficial { get; set; }
        public bool IsPrivate { get; set; }

        public IEnumerable<SelectListItem> Format
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Round Robin", Value = "1"},
                    new SelectListItem() {Text = "Swiss", Value = "2"},
                    new SelectListItem() {Text = "Playoff SE", Value = "3"},
                    new SelectListItem() {Text = "Playoff DE", Value = "4"}
                };
            }
        }


    }
}