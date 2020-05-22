using System.ComponentModel;

namespace RTA_Project_DAL.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string GuidKey { get; set; }
        [DefaultValue(null)]
        public string AccountId { get; set; }
        public string Name { get; set; }

        public virtual PlayerStats Stats { get; set; }
    }

}
