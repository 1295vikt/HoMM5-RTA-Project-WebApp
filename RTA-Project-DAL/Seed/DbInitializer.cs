using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace RTA_Project_DAL.Seed
{
    class DbInitializer : CreateDatabaseIfNotExists<RTADatabaseContext>
    {
        protected override void Seed(RTADatabaseContext context)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;

            // Heroes
            var lines = File.ReadAllLines(currentPath + "/SeedFiles/Heroes.txt");
            foreach (var line in lines)
            {
                string[] heroData = line.Split('\t');
                var hero = new Hero
                {
                    FactionId = byte.Parse(heroData[0]),
                    NameEng = heroData[1],
                    NameRus = heroData[2]
                };

                context.Heroes.Add(hero);
            }

            // Players & Rating
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

                context.Players.Add(player);
            }

            context.SaveChanges();


            // Factions from GoogleDocs
            var factionDictRus = new Dictionary<string, byte>
            {
                {"Маги", 1 },
                {"Лига", 2 },
                {"Гномы", 3 },
                {"Люди", 4 },
                {"Демоны", 5 },
                {"Некроманты", 6 },
                {"Орки", 7 },
                {"Эльфы", 8 }
            };


            //Tournaments from HLReport data
            //var tournamentList = ParseHLReport(currentPath + "/SeedFiles/HLReport_Tournaments.txt");

            var tournamentList = HLReportParser.ParseHLReport(currentPath + "/SeedFiles/HLReport_Tournaments.txt", context);


            //Tournament from GoogleDocs data
            var tournamentData = File.ReadAllText(currentPath + "/SeedFiles/Masters2019.txt").Split('@');
            var tournamentPlayersList = tournamentData[0].Split('\t').Select(name => new TournamentPlayer
            {
                Player = context.Players.FirstOrDefault(p => p.Name == name)
            }).ToList();

            var tournamentMatchData = tournamentData[1].Split(';');

            var tournamentMatches = new List<Match>();
            foreach (var item in tournamentMatchData)
            {
                var matchData = item.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                string playerName = matchData[0];
                var player1 = context.Players.First(p => p.Name == playerName);

                playerName = matchData[1];
                var player2 = context.Players.First(p => p.Name == playerName);

                var games = new List<Game>();
                for (int i = 8; i < matchData.Length; i++)
                {
                    var gameData = matchData[i].Split('\t');
                    var hero1Name = gameData[5];
                    var hero2Name = gameData[7];

                    bool player1Won = gameData[2] == ">";

                    var faction1Id = factionDictRus[gameData[4]];
                    var faction2Id = factionDictRus[gameData[6]];

                    var hero1 = context.Heroes.FirstOrDefault(h => h.NameRus == hero1Name).Id;
                    var hero2 = context.Heroes.FirstOrDefault(h => h.NameRus == hero2Name).Id;

                    var game = new Game
                    {
                        DateSubmitted = DateTime.ParseExact(gameData[0], "dd.MM.yyyy HH:mm:ss", null),

                        Player1Won = player1Won,

                        Faction1Id = faction1Id,
                        Hero1Id = hero1,

                        Faction2Id = faction2Id,
                        Hero2Id = hero2,

                        IsConfirmed = true
                    };

                    SeedStatsUpdater.UpdateStats(player1.Stats, player2.Stats, faction1Id, faction2Id, player1Won);
                    context.SaveChanges();

                    games.Add(game);

                }

                var match = new Match
                {
                    Player1Id = player1.Id,
                    Player2Id = player2.Id,

                    IsFinished = true,
                    IsBestOfFormat = true,

                    NumberOfGames = int.Parse(matchData[2]),
                    Player1Score = int.Parse(matchData[3]),
                    Player2Score = int.Parse(matchData[4]),

                    Bracket = new TournamentBracket
                    {
                        BracketTag = matchData[5],
                        NextBracketTagWinner = matchData[6],
                        NextBracketTagLoser = matchData[7]
                    },

                    Games = games
                };

                match.IsTechnicalResult = (match.NumberOfGames / 2 + 1) != match.Player1Score;
                if (match.Games.Count != 0)
                    match.DateFinished = match.Games.Last().DateSubmitted;

                tournamentMatches.Add(match);
            }

            var masters2019 = new Tournament
            {
                NameRus = "Турнир Masters 2019",
                NameEng = "Masters Tournament 2019",
                Map = new Map
                {
                    Version = "RTA 16.1",
                    ChangelogRus = "[Список изменений]"
                },
                Year = 2019,

                IsOfficial = true,
                IsFinished = true,
                IsPrivate = true,

                DateCreated = new DateTime(2019, 8, 26, 12, 10, 00),

                Description = new TournamentDescription
                {
                    ContentRus = File.ReadAllText(currentPath + "/SeedFiles/description-SpringTactics2020.txt"),
                },

                TournamentPlayers = tournamentPlayersList,

                TournamentGroups = new List<TournamentGroup>
                {
                    new TournamentGroup
                    {
                        GroupFormatId = 4,
                        NameRus = "Плей-офф",
                        NameEng = "Playoffs",
                        TournamentGroupPlayers = new List<TournamentGroupPlayer>(tournamentPlayersList.Select(tp => new TournamentGroupPlayer
                        {
                            TournamentPlayer = tp
                        })),
                        Matches = tournamentMatches,
                        IsFinished = true
                    }
                }
            };

            tournamentList.Add(masters2019);


            var springTactics2020 = new Tournament
            {
                NameRus = "Весенний Тактический Турнир 2020",
                NameEng = "Spring Tactics Tournament 2020",

                Year = 2020,
                DateCreated = new DateTime(2020, 3, 21),

                IsOfficial = true,
                IsSeasonal = true,
                Season = 2,

                TournamentGroups = new List<TournamentGroup>
                {
                    new TournamentGroup
                    {
                        NameRus = "Группа A",
                        GroupFormatId = 1,
                    },
                    new TournamentGroup
                    {
                        NameRus = "Группа B",
                        GroupFormatId = 1,
                    },
                    new TournamentGroup
                    {
                        NameRus = "Группа C",
                        GroupFormatId = 1,
                    },
                    new TournamentGroup
                    {
                        NameRus = "Группа D",
                        GroupFormatId = 1,
                    },
                    new TournamentGroup
                    {
                        NameRus = "Плей-офф",
                        GroupFormatId = 4,
                    },
                },

                Map = new Map
                {
                    Version = "RTA 16.4",
                    ChangelogRus = File.ReadAllText(currentPath + "/SeedFiles/changelog-RTA16.4.txt"),
                    DownloadLinkRus = "http://heroesleague.ru/public/homm5/rta/maps/RTA16.4.h5m"
                },

                Description = new TournamentDescription
                {
                    ContentRus = File.ReadAllText(currentPath + "/SeedFiles/description-SpringTactics2020.txt"),
                },

                HostsId = "391a1272-0a44-4749-8962-27f5c49d2a0d"

            };

            tournamentList.Add(springTactics2020);

            foreach (var tourn in tournamentList)
                context.Tournaments.Add(tourn);

            // News
            var articles = new List<Article>
            {
                new Article
                {
                    Title = "Турнир Masters 2019 завершен!",
                    LangId = 1,
                    Content = File.ReadAllText(currentPath+"/SeedFiles/article-masters2019.txt"),
                    AuthorId = "391a1272-0a44-4749-8962-27f5c49d2a0d",
                    Date = new DateTime(2019, 10, 22)
                },
                new Article
                {
                    Title = "RTA обновлена до версии 2.0",
                    LangId = 1,
                    Content = File.ReadAllText(currentPath+"/SeedFiles/article-RTA2.0.txt"),
                    AuthorId = "391a1272-0a44-4749-8962-27f5c49d2a0d",
                    Date = new DateTime(2020, 5, 2)
                },
                new Article
                {
                    Title = "Открыта регистрация на Весенний Тактический Турнир 2020",
                    LangId = 1,
                    Content = File.ReadAllText(currentPath+"/SeedFiles/article-SpringTactics2020.txt"),
                    AuthorId = "391a1272-0a44-4749-8962-27f5c49d2a0d",
                    Date = new DateTime(2020, 3, 21)
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

