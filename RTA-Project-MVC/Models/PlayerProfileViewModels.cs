using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class PlayerProfileViewModel
    {
        public string Name { get; set; }
        public PlayerStatsViewModel Stats { get; set; }
    }

    public class PlayerProfileCreateModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Никнейм")]
        [StringLength(20, ErrorMessage = "Допустимая длина: {2}-{1} символов", MinimumLength = 3)]
        [Remote("CheckIfNameExists", "Profile", ErrorMessage = "Игрок с таким именем уже существует")]
        public string Nickname { get; set; }

    }

    public class PlayerProfileLinkModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Ключ")]
        [StringLength(36, ErrorMessage = "Неверная длина ключа", MinimumLength = 36)]
        [Remote("CheckIfKeyExists", "Profile", ErrorMessage = "Игрок с данным ключом не найден")]
        public string GuidKey { get; set; }
    }

    public class PlayerStatsViewModel
    {
        [Display(Name = "Класс")]
        public string RatingClass { get; set; }

        [Display(Name = "Текущий рейтинг")]
        public int RatingPointsCurrent { get; set; }

        [Display(Name = "Максимальный рейтинг")]
        public int RatingPointsMax { get; set; }

        [Display(Name = "Золотые медали")]
        public int GoldMedals { get; set; }

        [Display(Name = "Серебрянные медали")]
        public int SilverMedals { get; set; }

        [Display(Name = "Бронзовые медали")]
        public int BronzeMedals { get; set; }

        [Display(Name = "Сыграно турниров")]
        public int TournamentExperience { get; set; }

    }
}