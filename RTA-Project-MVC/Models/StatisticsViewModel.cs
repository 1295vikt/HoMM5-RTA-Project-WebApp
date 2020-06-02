using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class StatisticsViewModel
    {

        public IEnumerable<SelectListItem> Factions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Академия", Value = "1"},
                    new SelectListItem() {Text = "Лига Теней", Value = "2"},
                    new SelectListItem() {Text = "Северные Кланы", Value = "3"},
                    new SelectListItem() {Text = "Орден порядка", Value = "4"},
                    new SelectListItem() {Text = "Инферно", Value = "5"},
                    new SelectListItem() {Text = "Некрополис", Value = "6"},
                    new SelectListItem() {Text = "Великая Орда", Value = "7"},
                    new SelectListItem() {Text = "Лесной Союз", Value = "8"}
                };
            }
        }

        public IEnumerable<SelectListItem> Heroes { get; set; }

        public IEnumerable<int> GamesVsFactionPlayed { get; set; }

        public IEnumerable<int> GamesVsFactionWon { get; set; }

        public IEnumerable<double> WinrateVsFaction { get; set; }

        public IDictionary<int, (int games, int wins)[]> HeroStats { get; set; }

    }

}