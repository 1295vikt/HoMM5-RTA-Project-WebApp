﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTA_Project_DAL.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int? ReportingAccountId { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public bool Player1Won { get; set; }
        [DefaultValue(false)]
        public bool IsConfirmed { get; set; }
        [DefaultValue(false)]
        public bool IsTechnicalWin { get; set; }

        public int Faction1Id { get; set; }
        public int Faction2Id { get; set; }

        [ForeignKey("Hero1")]
        public int Hero1Id { get; set; }
        [ForeignKey("Hero2")]
        public int Hero2Id { get; set; }

        public Hero Hero1 { get; set; }
        public Hero Hero2 { get; set; }

        public string Player1Comment { get; set; }
        public string Player2Comment { get; set; }
    }
}
