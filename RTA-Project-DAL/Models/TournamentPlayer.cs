using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class TournamentPlayer
    {
        public int Id { get; set; }
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public int TournamentGroupId { get; set; }
        [DefaultValue(false)]
        public bool Disqualified { get; set; }

        public virtual Player Player { get; set; }
    }
}
