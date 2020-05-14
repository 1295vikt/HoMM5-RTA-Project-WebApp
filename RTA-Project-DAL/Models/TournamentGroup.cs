using System.Collections.Generic;
using System.ComponentModel;

namespace RTA_Project_DAL.Models
{

    public class TournamentGroup
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }
        public byte GroupFormatId { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        [DefaultValue(false)]
        public bool IsFinished { get; set; }

        public virtual List<TournamentPlayer> TournamentPlayers { get; set; }
        public virtual List<Match> Matches { get; set; }
    }

}
