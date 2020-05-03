using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL
{
    class DbInitializer : DropCreateDatabaseAlways<RTADatabaseContext>
    {
        protected override void Seed(RTADatabaseContext context)
        {
            string[] lines = File.ReadAllLines("C:/GithubProjects/1295vikt/HoMM5-RTA-Project-WebApp/RTA-Project-DAL/SeedFiles/Players.txt");
            for (int i=0; i<lines.Length; i++)
            {
                string[] playerData = lines[i].Split('\t');
                //
            }           
        }
    }
}
