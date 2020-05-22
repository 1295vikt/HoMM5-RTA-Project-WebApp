namespace RTA_Project_BL.Models
{
    class TournamentBracketBL
    {
        public int Id { get; set; }
        public string BracketTag { get; set; }
        public string NextBracketTagWinner { get; set; }
        public string NextBracketTagLoser { get; set; }
    }
}
