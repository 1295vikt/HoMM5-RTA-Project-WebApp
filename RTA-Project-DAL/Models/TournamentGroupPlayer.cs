using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTA_Project_DAL.Models
{
    public class TournamentGroupPlayer
    {
        public int Id { get; set; }
        [ForeignKey("TournamentPlayer")]
        public int TournamentPlayerId { get; set; }

        public int GroupScore { get; set; }
        public float? Coefficient { get; set; }

        [DefaultValue(false)]
        public bool IsDisqualified { get; set; }

        public TournamentPlayer TournamentPlayer { get; set; }
    }
}
