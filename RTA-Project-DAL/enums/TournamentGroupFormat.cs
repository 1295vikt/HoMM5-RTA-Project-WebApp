using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.enums
{
    public enum TournamentGroupFormat : byte
    {
        RoundRobin = 1,
        Swiss = 2,
        PlayoffSingle = 3,
        PlayoffDouble = 4
    }
}
