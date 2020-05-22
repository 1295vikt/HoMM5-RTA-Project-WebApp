using System.Collections.Generic;

namespace RTA_Project_BL.Models
{
    public class TournamentPlayerBL
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }

        public PlayerBL Player { get; set; }
        public virtual ICollection<TournamentGroupPlayerBL> RelatedGroupPlayers { get; set; }
    }
}
