using RTA_Project_DAL.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int ReportingPlayerId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool ReportingPlayerWon { get; set; }
        [DefaultValue(false)]
        public bool IsConfirmed { get; set; }
        [DefaultValue(false)]
        public bool IsTechnicalWin { get; set; }

        public Faction Faction1Id { get; set; }
        public Faction Faction2Id { get; set; }

        [ForeignKey("Hero1")]
        public int Hero1Id { get; set; }
        [ForeignKey("Hero2")]
        public int Hero2Id { get; set; }

        public virtual Hero Hero1 { get; set; }
        public virtual Hero Hero2 { get; set; }
    }
}
