namespace RTA_Project_BL.Models
{
    public class PlayerBL
    {

        public int Id { get; set; }
        public string GuidKey { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }

        public PlayerStatsBL Stats { get; set; }

    }

}
