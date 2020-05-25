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

        public int GamesAsAcademy { get; set; }
        public int GamesAsDungeon { get; set; }
        public int GamesAsFortress { get; set; }
        public int GamesAsHaven { get; set; }
        public int GamesAsInferno { get; set; }
        public int GamesAsNecropolis { get; set; }
        public int GamesAsStronghold { get; set; }
        public int GamesAsSylvan { get; set; }

        public int WinsAsAcademy { get; set; }
        public int WinsAsDungeon { get; set; }
        public int WinsAsFortress { get; set; }
        public int WinsAsHaven { get; set; }
        public int WinsAsInferno { get; set; }
        public int WinsAsNecropolis { get; set; }
        public int WinsAsStronghold { get; set; }
        public int WinsAsSylvan { get; set; }

        public double WinrateAsAcademy => GetWinPercent(GamesAsAcademy, WinsAsAcademy);
        public double WinrateAsDungeon => GetWinPercent(GamesAsDungeon, WinsAsDungeon);
        public double WinrateAsFortress => GetWinPercent(GamesAsFortress, WinsAsFortress);
        public double WinrateAsHaven => GetWinPercent(GamesAsHaven, WinsAsHaven);
        public double WinrateAsInferno => GetWinPercent(GamesAsInferno, WinsAsInferno);
        public double WinrateAsNecropolis => GetWinPercent(GamesAsNecropolis, WinsAsNecropolis);
        public double WinrateAsStronghold => GetWinPercent(GamesAsStronghold, WinsAsStronghold);
        public double WinrateAsSylvan => GetWinPercent(GamesAsSylvan, WinsAsSylvan);

        private double GetWinPercent(int games, int wins)
        {
            return games == 0 ? 0 : Math.Round((double)(wins * 100) / games, 2);
        }
    }
}
