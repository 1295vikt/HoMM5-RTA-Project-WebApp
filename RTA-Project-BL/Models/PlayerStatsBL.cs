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
        public int GamesLost => GamesPlayed - GamesWon;
        public double Winrate => Math.Round((double)(GamesWon / GamesPlayed), 2);

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

    }
}
