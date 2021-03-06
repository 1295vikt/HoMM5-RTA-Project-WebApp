﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTA_Project_DAL.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        [ForeignKey("Map")]
        public string MapVersion { get; set; }
        public int Year { get; set; }
        public int Season { get; set; }

        public DateTime? DateCreated { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }
        [DefaultValue(false)]
        public bool IsFinished { get; set; }
        [DefaultValue(false)]
        public bool IsOfficial { get; set; }
        [DefaultValue(false)]
        public bool IsSeasonal { get; set; }
        [DefaultValue(false)]
        public bool IsPrivate { get; set; }
        [DefaultValue(false)]
        public bool IsArchived { get; set; }

        public string HostsId { get; set; }

        public virtual ICollection<TournamentGroup> TournamentGroups { get; set; }
        public virtual ICollection<TournamentPlayer> TournamentPlayers { get; set; }

        public Map Map { get; set; }
        public TournamentDescription Description { get; set; }

    }
}
