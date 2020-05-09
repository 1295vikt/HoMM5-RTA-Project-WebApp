using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class TournamentDescription
    {
        [Key, ForeignKey("Tournament")]
        public int Id { get; set; }
        public string Content { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
