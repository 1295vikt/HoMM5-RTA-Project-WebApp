using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class TournamentGroupBL
    {
        public int Id { get; set; }
        public byte GroupFormatId { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }

        public int GroupNum { get; set; }

        public List<TournamentPlayerBL> TournamentPlayers { get; set; }
        public List<MatchBL> Matches { get; set; }
    }
}
