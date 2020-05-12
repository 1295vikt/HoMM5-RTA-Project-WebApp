using System.Collections.Generic;

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

        public bool IsActive { get; set; }
        public bool IsInSecondStage { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsSeasonal { get; set; }
        public bool IsPrivate { get; set; }


        public List<TournamentGroupBL> TournamentGroups { get; set; }
        public List<TournamentPlayerBL> TournamentPlayers { get; set; }
        public List<PlayerBL> Hosts { get; set; }


        public TournamentDescriptionBL Description { get; set; }
    }
}
