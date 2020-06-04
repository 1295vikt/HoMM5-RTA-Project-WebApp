using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class StatisticsFactionHeroesModel
    {

        public int[] GamesVsFactionPlayed { get; set; }
        public int[] GamesVsFactionWon { get; set; }
        public int[] GamesVsFactionLost { get; set; }
        public double[] WinrateVsFaction { get; set; }

        public IList<int[]> GamesVsFactionPlayedHero { get; set; }
        public IList<int[]> GamesVsFactionWonHero { get; set; }
        public IList<int[]> GamesVsFactionLostHero { get; set; }
        public IList<double[]> WinrateVsFactionHero { get; set; }

        public string[] HeroNames { get; set; }

    }

    public class StatisticsAllFactionsModel
    {

        public int[,] GamesPlayed { get; set; }
        public int[,] GamesWon { get; set; }
        public int[,] GamesLost { get; set; }
        public double[,] Winrate { get; set; }

        public string[] Factions
        {
            get
            {
                return new string[]
                {
                    "Академия",
                    "Лига Теней",
                    "Северные Кланы",
                    "Орден порядка",
                    "Инферно",
                    "Некрополис",
                    "Великая Орда",
                    "Лесной Союз",
                    "Всего"
                };
            }
        }
    }

    public class StatisticsFilterModel
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


        public bool GetForSingleFaction { get; set; }
        public int FactionId { get; set; }

        public bool FilterByDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Не указана начальная дата")]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Не указана конечная дата")]
        public DateTime DateTo { get; set; }

    }

}