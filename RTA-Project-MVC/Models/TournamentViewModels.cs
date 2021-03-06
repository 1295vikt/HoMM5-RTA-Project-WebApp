﻿using RTA_Project_DAL.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class TournamentCreateModel
    {
        [Required(ErrorMessage="Поле является обязательным для заполнения")]
        [Display(Name = "Название турнира")]
        [StringLength(40, ErrorMessage = "{0} должно содержать от {2} до {1} символов", MinimumLength = 8)]
        public string NameRus { get; set; }

        public string NameEng { get; set; }

        [Required(ErrorMessage = "Поле является обязательным для заполнения")]
        [Display(Name = "Версия карты")]
        [Remote("CheckIfVersionExists", "RTA", ErrorMessage = "Указонной версии карты не существует")]
        public string MapVersion { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }

        [Display(Name = "Сезон")]
        public int Season { get; set; }


        [Display(Name = "Двухэтапный турнир")]
        public bool Is2Stage { get; set; }

        public TournamentGroupFormat Stage1Format { get; set; }
        public TournamentGroupFormat Stage2Format { get; set; }


        [Display(Name = "Официальный турнир")]
        public bool IsOfficial { get; set; }

        [Display(Name = "Сезонный турнир")]
        public bool IsSeasonal { get; set; }

        [Display(Name = "Закрытый турнир")]
        public bool IsPrivate { get; set; }


        [Required(AllowEmptyStrings=false, ErrorMessage = "{0} не должно быть пустым")]
        [MaxLength(10000)]
        [Display(Name = "Описание турнира")]
        public string ContentRus { get; set; }

        public IList<SelectListItem> HostsAvailable { get; set; }

        public IEnumerable<SelectListItem> Formats
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Round Robin", Value = "1"},
                    new SelectListItem() {Text = "Swiss", Value = "2"},
                    new SelectListItem() {Text = "Playoff SE", Value = "3"},
                    new SelectListItem() {Text = "Playoff DE", Value = "4"},
                    new SelectListItem() {Text = "Особый", Value = "5"},
                };
            }
        }

        public IEnumerable<SelectListItem> Seasons
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Зимний", Value = "1"},
                    new SelectListItem() {Text = "Весенний", Value = "2"},
                    new SelectListItem() {Text = "Летний", Value = "3"},
                    new SelectListItem() {Text = "Осенний", Value = "4"}
                };
            }
        }

    }

    public class TournamentPreviewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название турнира")]
        public string NameRus { get; set; }
        public string NameEng { get; set; }

        [Display(Name = "Версия карты")]
        public string MapVersion { get; set; }

        public bool IsActive { get; set; }
        public bool IsFinished { get; set; }


        public bool IsOfficial { get; set; }
        public bool IsSeasonal { get; set; }
        public bool IsPrivate { get; set; }
    }

    public class TournamentDetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "Название турнира")]
        public string NameRus { get; set; }
        public string NameEng { get; set; }

        [Display(Name = "Версия карты")]
        public string MapVersion { get; set; }

        public bool IsActive { get; set; }
        public bool IsFinished { get; set; }    

        public bool IsOfficial { get; set; }
        public bool IsSeasonal { get; set; }
        public bool IsPrivate { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Организаторы")]
        public IEnumerable<string> TournamentHosts { get; set; }

        [Display(Name = "Участники")]
        public IEnumerable<string> TournamentPlayers { get; set; }

        [Display(Name = "Описание")]
        public string Content { get; set; }

    }

}