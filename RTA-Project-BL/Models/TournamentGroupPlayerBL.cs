using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class TournamentGroupPlayerBL
    {
        public int Id { get; set; }
        public int TournamentPlayerId { get; set; }

        public int GroupScore { get; set; }
        public float? Coefficient { get; set; }

        public bool IsDisqualified { get; set; }

    }
}
