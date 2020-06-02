using System;

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

        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public double Winrate => GetWinPercent(GamesPlayed, GamesWon);


        public int[] GamesPlayedFaction { get; set; }
        public int[] GamesWonFaction { get; set; }
        public double[] WinrateFaction
        {
            get
            {
                double[] wr = new double[8];

                for (int i=0;i<8;i++)
                    wr[i] = GetWinPercent(GamesPlayedFaction[i], GamesWonFaction[i]);

                return wr;
            }
        }

        private double GetWinPercent(int games, int wins)
        {
            return games == 0 ? 0 : Math.Round((double)(wins * 100) / games, 2);
        }

    }
}
