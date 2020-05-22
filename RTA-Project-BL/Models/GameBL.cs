using System;

namespace RTA_Project_BL.Models
{
    public class GameBL
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int ReportingPlayerId { get; set; }

        public DateTime? DateSubmitted { get; set; }
        public bool ReportingPlayerWon { get; set; }
        public bool IsTechnicalWin { get; set; }

        public byte Faction1Id { get; set; }
        public byte Faction2Id { get; set; }

        public virtual HeroBL Hero1 { get; set; }
        public virtual HeroBL Hero2 { get; set; }

        public string Player1Comment { get; set; }
        public string Player2Comment { get; set; }
    }
}
