using System;
using System.Collections.Generic;
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
        public int Year { get; set; }

        public bool IsActive { get; set; }
        public bool IsFinished { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsPrivate { get; set; }

        public virtual List<TournamentGroup> TournamentGroups { get; set; }

    }
}
