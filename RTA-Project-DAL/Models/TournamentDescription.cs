using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTA_Project_DAL.Models
{
    public class TournamentDescription
    {
        [Key, ForeignKey("Tournament")]
        public int Id { get; set; }

        public string ContentRus { get; set; }
        public string ContentEng { get; set; }

        public Tournament Tournament { get; set; }
    }
}
