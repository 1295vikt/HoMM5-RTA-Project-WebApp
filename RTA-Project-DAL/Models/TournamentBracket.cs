using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class TournamentBracket
    {
        [Key, ForeignKey("Match")]
        public int Id { get; set; }
        public string BracketTag { get; set; }
        public string NextBracketTagWinner { get; set; }
        public string NextBracketTagLoser { get; set; }

        public virtual Match Match { get; set; }
    }
}
