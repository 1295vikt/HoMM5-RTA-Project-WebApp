using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class PlayerProfileViewModel
    {
        public string Name { get; set; }

        //Rating
        [Display(Name = "Класс")]
        public string RatingClass { get; set; }

        [Display(Name = "Текущий рейтинг")]
        public int RatingPointsCurrent { get; set; }

        [Display(Name = "Макс. рейтинг")]
        public int RatingPointsMax { get; set; }

        [Display(Name = "1-е места")]
        public int GoldMedals { get; set; }

        [Display(Name = "2-е места")]
        public int SilverMedals { get; set; }

        [Display(Name = "3-е места")]
        public int BronzeMedals { get; set; }

        [Display(Name = "Опыт")]
        public int TournamentExperience { get; set; }

        //TODO: show list of tournaments, link awards to tournaments

        //General stats
        [Display(Name = "Количество игр")]
        public int GamesPlayed { get; set; }

        [Display(Name = "Побед")]
        public int GamesWon { get; set; }

        [Display(Name = "Процент побед")]
        public string Winrate { get; set; }

        //Faction-specific stats

        public IEnumerable<int> GamesPlayedFaction { get; set; }
        public IEnumerable<int> GamesWonFaction { get; set; }
        public IEnumerable<double> WinrateFaction { get; set; }

    }

    public class PlayerProfileCreateModel
    {
        [Display(Name = "Никнейм")]
        [StringLength(20, ErrorMessage = "Допустимая длина: {2}-{1} символов", MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Zа-яА-Я0-9_ ]+$", ErrorMessage = @"Имя содержит недопустимые символы")]
        [Remote("CheckIfNameExists", "Profile", ErrorMessage = "Игрок с таким именем уже существует")]
        public string Nickname { get; set; }

    }


    public class PlayerProfileLinkModel
    {
        [Display(Name = "Ключ")]
        [StringLength(36, ErrorMessage = "Неверная длина ключа", MinimumLength = 36)]
        [Remote("CheckIfKeyExists", "Profile", ErrorMessage = "Игрок с данным ключом не найден")]
        public string GuidKey { get; set; }
    }

}