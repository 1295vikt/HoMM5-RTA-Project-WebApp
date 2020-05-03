using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class TournamentDescription
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Content { get; set; }
    }
}
