using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int TournamentGroupId { get; set; }
        public bool IsFinished { get; set; }
        public DateTime DateFinished { get; set; }

        [ForeignKey("Player1")]
        public int Player1ID { get; set; }
        [ForeignKey("Player2")]
        public int Player2ID { get; set; }

        public virtual Player Player1 { get; set; }
        public virtual Player Player2 { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
