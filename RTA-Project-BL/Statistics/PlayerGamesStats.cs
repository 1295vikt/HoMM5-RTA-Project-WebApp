using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Statistics
{
    class PlayerGamesStats
    {

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }
        public int GamesLost { get; set; }

        public double Winrate { get; set; }

        public string FavouriteFaction { get; set; }

    }
}
