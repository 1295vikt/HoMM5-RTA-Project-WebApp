using System.ComponentModel;

namespace RTA_Project_DAL.Models
{

    public class Hero
    {
        public int Id { get; set; }
        public int FactionId { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
        [DefaultValue(false)]
        public bool IsRemovedFromHeroPool { get; set; }
    }

}
