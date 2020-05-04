using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Models
{
    public class PlayerBL
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }

        public PlayerStatsBL Stats { get; set; }
    }

}
