using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace RTA_Project_DAL
{
    class DbInitializer : CreateDatabaseIfNotExists<RTADatabaseContext>
    {
        protected override void Seed(RTADatabaseContext context)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;

            var heroes = new List<Hero>();
            string[] lines = File.ReadAllLines(currentPath + "/SeedFiles/Heroes.txt");
            foreach (var line in lines)
            {
                string[] heroData = line.Split('\t');
                var hero = new Hero
                {
                    FactionId = byte.Parse(heroData[0]),
                    NameEng = heroData[1],
                    NameRus = heroData[2]
                };

                heroes.Add(hero);
            }

            foreach (var hero in heroes)
            {
                context.Heroes.Add(hero);
            }

            var players = new List<Player>();
            lines = File.ReadAllLines(currentPath + "/SeedFiles/Players.txt");

            foreach (var line in lines)
            {
                string[] playerData = line.Split('\t');
                var player = new Player()
                {
                    Name = playerData[0],
                    AccountId = null,
                    GuidKey = Guid.NewGuid().ToString(),
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
