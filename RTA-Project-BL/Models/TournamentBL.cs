using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class TournamentBL
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        public string MapVersion { get; set; }
        public int Year { get; set; }
        public byte Season { get; set; }

        public bool IsOfficial { get; set; }
        public bool IsPrivate { get; set; }

        public virtual List<TournamentGroupBL> TournamentGroups { get; set; }
        public virtual List<TournamentPlayerBL> TournamentPlayers { get; set; }
    }
}
