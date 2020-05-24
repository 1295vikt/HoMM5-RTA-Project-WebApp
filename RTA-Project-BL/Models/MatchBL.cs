using System;
using System.Collections.Generic;

namespace RTA_Project_BL.Models
{
    public class MatchBL
    {
        public int Id { get; set; }
        public int TournamentGroupId { get; set; }
        public int NumberOfGames { get; set; }

        public bool IsBestOfFormat { get; set; }
        public bool IsTechnicalResult { get; set; }
        public bool IsSpecialMatch { get; set; }

        public int Player1Id { get; set; }
        public int Player2Id { get; set; }

        public string Player1Name { get; set; }
        public string Player2Name { get; set; }

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public DateTime? DateFinished { get; set; }   

        public ICollection<GameBL> Games { get; set; }
    }
}
