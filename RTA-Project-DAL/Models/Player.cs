using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Player
    {
        public int Id { get; set; }
        public Guid GuidKey { get; set; }
        [DefaultValue(null)]
        public string AccountId { get; set; }
        public string Name { get; set; }

        public virtual PlayerStats Stats { get; set; }
    }

}
