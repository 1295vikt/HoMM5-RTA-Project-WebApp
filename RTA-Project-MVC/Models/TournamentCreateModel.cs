﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTA_Project_MVC.Models
{
    public class TournamentCreateModel
    {
        [Required]
        [MaxLength(40, ErrorMessage = "Название не должно содержать более 40 символов")]
        public string NameRus { get; set; }
        [MaxLength(40, ErrorMessage = "Название не должно содержать более 40 символов")]
        public string NameEng { get; set; }
        public string MapVersion { get; set; }
        public int Year { get; set; }
        public byte Season { get; set; }

        public bool Is2Stage { get; set; }

        public byte Stage1Format { get; set; }
        public byte Stage2Format { get; set; }

        public bool IsOfficial { get; set; }
        public bool IsSeasonal { get; set; }
        public bool IsPrivate { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> Formats
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
}