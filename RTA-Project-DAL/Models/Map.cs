using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Map
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Version { get; set; }
        public string ChangelogRus { get; set; }
        public string ChangelogEng { get; set; }
        public string DownloadLinkRus { get; set; }
        public string DownloadLinkEng { get; set; }
    }
}
