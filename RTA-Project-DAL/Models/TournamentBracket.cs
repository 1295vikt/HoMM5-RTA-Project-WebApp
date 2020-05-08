﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class TournamentBracket
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string BracketTag { get; set; }
        public string NextBracketTagWinner { get; set; }
        public string NextBracketTagLoser { get; set; }
    }
}