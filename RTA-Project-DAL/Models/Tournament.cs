using RTA_Project_DAL.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        public string MapVersion { get; set; }
        public int Year { get; set; }
        public Season Season { get; set; }        

        [DefaultValue(false)]
        public bool IsActive { get; set; }
        [DefaultValue(false)]
        public bool IsInPlayoffStage { get; set; }
        [DefaultValue(false)]
        public bool IsFinished { get; set; }
        [DefaultValue(false)]
        public bool IsOfficial { get; set; }
        [DefaultValue(false)]
        public bool IsSeasonal { get; set; }
        [DefaultValue(false)]
        public bool IsPrivate { get; set; }

        public virtual List<TournamentGroup> TournamentGroups { get; set; }
        public virtual List<TournamentPlayer> TournamentPlayers { get; set; }

        public virtual TournamentDescription Description { get; set; }

    }
}
