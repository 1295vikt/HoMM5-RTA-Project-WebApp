﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public int FactionId { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }
    }
}