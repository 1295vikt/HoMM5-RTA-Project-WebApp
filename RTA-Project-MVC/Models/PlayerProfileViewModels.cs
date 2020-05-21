using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class PlayerProfileViewModel
    {
        public string Name { get; set; }

        //from Stats
        [Display(Name = "Класс")]
        public string RatingClass { get; set; }

        [Display(Name = "Текущий рейтинг")]
        public int RatingPointsCurrent { get; set; }

        [Display(Name = "Макс. рейтинг")]
        public int RatingPointsMax { get; set; }

        [Display(Name = "Золото")]
        public int GoldMedals { get; set; }

        [Display(Name = "Серебро")]
        public int SilverMedals { get; set; }

        [Display(Name = "Бронза")]
        public int BronzeMedals { get; set; }

        [Display(Name = "Опыт")]
        public int TournamentExperience { get; set; }

        //TODO: show list of tournaments, link awards to tournaments

        //from Games
        [Display(Name = "Количество игр")]
        public int GamesPlayed { get; set; }

        [Display(Name = "Побед")]
        public int GamesWon { get; set; }

        [Display(Name = "Поражений")]
        public int GamesLost { get; set; }

        [Display(Name = "Процент побед")]
        public double Winrate { get; set; }

        [Display(Name = "Любимая фракция")]
        public string FavouriteFaction { get; set; }
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

}