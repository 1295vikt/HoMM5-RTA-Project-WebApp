using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTA_Project_DAL.Models
{
    public class TournamentPlayer
    {

        public int Id { get; set; }
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<TournamentGroupPlayer> RelatedGroupPlayers { get; set; }
    }
}
