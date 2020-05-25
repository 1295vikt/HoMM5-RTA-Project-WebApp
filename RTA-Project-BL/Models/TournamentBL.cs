using System;
using System.Collections.Generic;

namespace RTA_Project_BL.Models
{
    public class TournamentBL
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        public int Year { get; set; }
        public byte Season { get; set; }

        public bool IsActive { get; set; }
        public bool IsInSecondStage { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsSeasonal { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsArchived { get; set; }

        public DateTime? DateCreated { get; set; }

        public string HostsId { get; set; }

        public ICollection<TournamentGroupBL> TournamentGroups { get; set; }
        public ICollection<TournamentPlayerBL> TournamentPlayers { get; set; }

        public MapBL Map { get; set; }

        public TournamentDescriptionBL Description { get; set; }
    }
}
