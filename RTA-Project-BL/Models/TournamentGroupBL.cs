using System.Collections.Generic;

namespace RTA_Project_BL.Models
{
    public class TournamentGroupBL
    {
        public int Id { get; set; }
        public int GroupFormatId { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }


        public ICollection<TournamentGroupPlayerBL> TournamentGroupPlayers { get; set; }
        public ICollection<MatchBL> Matches { get; set; }
    }
}
