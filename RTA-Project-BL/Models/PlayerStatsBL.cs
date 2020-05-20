using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class PlayerStatsBL
    {
        public int Id { get; set; }
        public string RatingClass { get; set; }
        public int RatingPointsCurrent { get; set; }
        public int RatingPointsMax { get; set; }
        public int GoldMedals { get; set; }
        public int SilverMedals { get; set; }
        public int BronzeMedals { get; set; }
        public int TournamentExperience { get; set; }
    }
}
