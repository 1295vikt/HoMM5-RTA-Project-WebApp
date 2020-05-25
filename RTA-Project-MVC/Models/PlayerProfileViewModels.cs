using System;
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
        public int GamesAsAcademy { get; set; }
        public int GamesAsDungeon { get; set; }
        public int GamesAsFortress { get; set; }
        public int GamesAsHaven { get; set; }
        public int GamesAsInferno { get; set; }
        public int GamesAsNecropolis { get; set; }
        public int GamesAsStronghold { get; set; }
        public int GamesAsSylvan { get; set; }

        public int WinsAsAcademy { get; set; }
        public int WinsAsDungeon { get; set; }
        public int WinsAsFortress { get; set; }
        public int WinsAsHaven { get; set; }
        public int WinsAsInferno { get; set; }
        public int WinsAsNecropolis { get; set; }
        public int WinsAsStronghold { get; set; }
        public int WinsAsSylvan { get; set; }

        public string WinrateAsAcademy { get; set; }
        public string WinrateAsDungeon { get; set; }
        public string WinrateAsFortress { get; set; }
        public string WinrateAsHaven { get; set; }
        public string WinrateAsInferno { get; set; }
        public string WinrateAsNecropolis { get; set; }
        public string WinrateAsStronghold { get; set; }
        public string WinrateAsSylvan { get; set; }
    }

    public class PlayerProfileCreateModel
    {
        [Display(Name = "Никнейм")]
        [StringLength(20, ErrorMessage = "Допустимая длина: {2}-{1} символов", MinimumLength = 3)]
        [RegularExpression(@"^[^\\/:\*\?\""<>\|;%$()@№#^&+=!~{}[\]]+$", ErrorMessage = @"Имя содержит недопустимые символы")]
        [Remote("CheckIfNameExists", "Profile", ErrorMessage = "Игрок с таким именем уже существует")]
        public string Nickname { get; set; }

    }
    //%$()@№#^+-=!~
    public class PlayerProfileLinkModel
    {
        [Display(Name = "Ключ")]
        [StringLength(36, ErrorMessage = "Неверная длина ключа", MinimumLength = 36)]
        [Remote("CheckIfKeyExists", "Profile", ErrorMessage = "Игрок с данным ключом не найден")]
        public string GuidKey { get; set; }
    }

}