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
        [DefaultValue(false)]
        public bool IsDisqualified { get; set; }

        public int GroupScore { get; set; }
        public float? Coefficient { get; set; }

        public virtual Player Player { get; set; }
    }
}
