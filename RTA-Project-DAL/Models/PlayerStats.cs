﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class PlayerStats
    {
        [Key, ForeignKey("Player")]
        public int Id { get; set; }

        public string RatingClass { get; set; }
        public int RatingPointsCurrent { get; set; }
        public int RatingPointsMax { get; set; }
        public int GoldMedals { get; set; }
        public int SilverMedals { get; set; }
        public int BronzeMedals { get; set; }
        public int TournamentExperience { get; set; }
        //TODO more stats

        public virtual Player Player { get; set; }
    }
}