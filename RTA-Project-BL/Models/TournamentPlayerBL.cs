using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class TournamentPlayerBL
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }

        public bool IsDisqualified { get; set; }

        public int GroupScore { get; set; }
        public float? Coefficient { get; set; }

        public PlayerBL Player { get; set; }
    }
}
