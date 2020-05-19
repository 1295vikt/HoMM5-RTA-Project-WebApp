using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTA_Project_MVC.Models
{
    public class PlayerProfileViewModel
    {

    }

    public class PlayerProfileCreateModel
    {
        public string Nickname { get; set; }
        public Guid GuidKey { get; set; }
    }
}