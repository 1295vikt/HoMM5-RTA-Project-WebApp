using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace RTA_Project_DAL
{
    class DbInitializer : CreateDatabaseIfNotExists<RTADatabaseContext>
    {
        protected override void Seed(RTADatabaseContext context)
        {

            var players = new List<Player>();

            string[] lines = File.ReadAllLines("C:/GithubProjects/1295vikt/HoMM5-RTA-Project-WebApp/RTA-Project-DAL/SeedFiles/Players.txt");

            for (int i = 0; i < lines.Length; i++)
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

            foreach (var player in players)
            {
                context.Players.Add(player);
            }


            var articles = new List<Article>
            {
                new Article
                {
                    Title = "RTA 2.0.2",
                    LangId = 1,
                    Content = "Доступна новая версия RTA 2.0.2, хвала Рехейвену! Lorem ipsum dolor sit amet!",
                    AuthorId = "bbab0a2e-7e19-46e7-9abe-bd80e5462d69",
                    Date = new DateTime(2020,5, 12, 15, 25, 30)
                },
                new Article
                {
                    Title = "Открыта регистрация на Командный Тактический Турнир 2020",
                    LangId = 1,
                    Content = "Командный Тактический Турнир - 2020 - круговой командный турнир, в котором может принять участие любой желающий.",
                    AuthorId = "bbab0a2e-7e19-46e7-9abe-bd80e5462d69",
                    Date = new DateTime(2020,5, 12, 16, 25, 30)
                },
            };

            foreach (var article in articles)
            {
                context.Articles.Add(article);
            }


            context.SaveChanges();
        }
    }
}
