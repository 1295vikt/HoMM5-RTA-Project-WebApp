using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class MatchBL
    {
        public int Id { get; set; }
        public int TournamentGroupId { get; set; }
        public int NumberOfGames { get; set; }
        public bool IsBestOfFormat { get; set; }

        public string Player1Id { get; set; }
        public string Player2Id { get; set; }

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public DateTime DateFinished { get; set; }

        public List<GameBL> Games { get; set; }
    }
}
