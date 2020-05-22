using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace RTA_Project_DAL
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
            var factionDict = new Dictionary<string, byte>
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

            //Tournaments
            var tournamentData = File.ReadAllText(currentPath + "/SeedFiles/Masters2019.txt").Split('@');
            var tournamentPlayers = tournamentData[0].Split('\t').Select(name => new TournamentPlayer
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
                    var game = new Game
                    {
                        DateSubmitted = DateTime.ParseExact(gameData[0], "dd.MM.yyyy HH:mm:ss", null),

                        ReportingPlayerId = player1.Id,
                        ReportingPlayerWon = gameData[2] == ">" ? true : false,

                        Faction1Id = factionDict[gameData[4]],
                        Hero1Id = context.Heroes.FirstOrDefault(h => h.NameRus == hero1Name).Id,

                        Faction2Id = factionDict[gameData[6]],
                        Hero2Id = context.Heroes.FirstOrDefault(h => h.NameRus == hero2Name).Id,

                        IsConfirmed = true
                    };
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
                    ContentRus = "Закрытый турнир для 16 сильнейших игроков в соответствии с Рейтингом RTA от alex_nv"
                },

                TournamentPlayers = tournamentPlayers,

                TournamentGroups = new List<TournamentGroup>
                {
                    new TournamentGroup
                    {
                        GroupFormatId = 4,
                        NameRus = "Плей-офф",
                        NameEng = "Playoffs",
                        TournamentGroupPlayers = new List<TournamentGroupPlayer>(tournamentPlayers.Select(tp => new TournamentGroupPlayer
                        {
                            TournamentPlayer = tp
                        })),
                        Matches = tournamentMatches,
                        IsFinished = true
                    }
                }
            };

            context.Tournaments.Add(masters2019);

            // News
            var articles = new List<Article>
            {
                new Article
                {
                    Title = "RTA 2.0.2",
                    LangId = 1,
                    Content = "Доступна новая версия RTA 2.0.2, хвала Редхейвену! Lorem ipsum dolor sit amet!",
                    AuthorId = "bbab0a2e-7e19-46e7-9abe-bd80e5462d69",
                    Date = new DateTime(2020, 5, 12, 15, 25, 30)
                },
                new Article
                {
                    Title = "Открыта регистрация на Командный Тактический Турнир 2020",
                    LangId = 1,
                    Content = "Командный Тактический Турнир - 2020 - круговой командный турнир, в котором может принять участие любой желающий.",
                    AuthorId = "bbab0a2e-7e19-46e7-9abe-bd80e5462d69",
                    Date = new DateTime(2020, 5, 12, 16, 25, 30)
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
