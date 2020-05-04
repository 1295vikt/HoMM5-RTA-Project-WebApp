using RTA_Project_DAL.Models;
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

            var players = new List<Player>();

            string[] lines = File.ReadAllLines("C:/GithubProjects/1295vikt/HoMM5-RTA-Project-WebApp/RTA-Project-DAL/SeedFiles/Players.txt");

            for (int i=0; i<lines.Length; i++)
            {
                string[] playerData = lines[i].Split('\t');
                var player = new Player()
                {
                    Name = playerData[0],
                    AccountId = null,
                    GuidKey = Guid.NewGuid(),
                    Stats = new PlayerStats
                    {
                        RatingClass = playerData[1],
                        RatingPointsCurrent = int.Parse(playerData[2]),
                        RatingPointsMax = int.Parse(playerData[3]),
                        GoldMedals = int.Parse(playerData[4]),
                        SilverMedals = int.Parse(playerData[5]),
                        BronzeMedals = int.Parse(playerData[6]),
                        TournamentExperience = int.Parse(playerData[7])
                    }
                };

                players.Add(player);
            } 
            
            foreach(var player in players)
            {
                context.Players.Add(player);
            }

            context.SaveChanges();
        }
    }
}
