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



            // Factions from HLReport
            var factionDictEng = new Dictionary<string, byte>
            {
                    { "Wizard", 1 },
                    { "Warlock", 2 },
                    { "Runemage", 3 },
                    { "Knight", 4 },
                    { "Inferno", 5 },
                    { "Necromancer", 6 },
                    { "Barbarian", 7 },
                    { "Ranger", 8 }
            };

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
            var HLReportTournaments = ParseHLReport(currentPath + "/SeedFiles/HLReport_Tournaments.txt");

            foreach (var tourn in HLReportTournaments)
                context.Tournaments.Add(tourn);

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
                    var game = new Game
                    {
                        DateSubmitted = DateTime.ParseExact(gameData[0], "dd.MM.yyyy HH:mm:ss", null),

                        ReportingPlayerId = player1.Id,
                        ReportingPlayerWon = gameData[2] == ">" ? true : false,

                        Faction1Id = factionDictRus[gameData[4]],
                        Hero1Id = context.Heroes.FirstOrDefault(h => h.NameRus == hero1Name).Id,

                        Faction2Id = factionDictRus[gameData[6]],
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



            //Parse methods
            List<Tournament> ParseHLReport(string filepath)
            {

                var tournaments = new List<Tournament>();
                var data = File.ReadAllText(filepath).Split(new[] { "@tourn@" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var entry in data)
                {

                    var tournEntry = entry.Split(new[] { "@group@" }, StringSplitOptions.RemoveEmptyEntries);

                    var tournamentGroups = new List<TournamentGroup>();
                    var tournamentPlayers = new List<TournamentPlayer>();

                    var tournData = tournEntry[0].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    var tournament = new Tournament
                    {
                        IsFinished = true,
                        IsOfficial = true,

                        NameRus = tournData[0],
                        NameEng = tournData[1],
                        Year = int.Parse(tournData[2]),
                        IsSeasonal = bool.Parse(tournData[3]),
                        Season = byte.Parse(tournData[4]),
                        IsPrivate = bool.Parse(tournData[5]),
                        DateCreated = DateTime.ParseExact(tournData[6], "yyyy.MM.dd", null),

                        Map = new Map
                        {
                            Version = tournData[7]
                        },

                        Description = new TournamentDescription
                        {
                            //ContentRus = 
                            //ContentEng = 
                        }

                    };

                    for (int i = 1; i < tournEntry.Length; i++)
                    {
                        var groupEntry = tournEntry[i].Split('#');
                        var groupData = groupEntry[0].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                        var group = new TournamentGroup
                        {
                            NameRus = groupData[0],
                            NameEng = groupData[1],
                            GroupFormatId = byte.Parse(groupData[2])
                        };


                        var matches = new List<Match>();
                        var tournamentGroupPlayers = new List<TournamentGroupPlayer>();

                        var matchData = groupEntry[1].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in matchData)
                        {
                            var matchEntry = line.Replace("[confirmed]", "").Replace("[/confirmed]", "").Replace("[open]", "").Replace("[/open]", "").Split(';');

                            var players = matchEntry[0].Split(' ');
                            var player1Name = players[0];
                            var player2Name = players[2];

                            var player1 = context.Players.FirstOrDefault(p => p.Name == player1Name);
                            var player2 = context.Players.FirstOrDefault(p => p.Name == player2Name);

                            if (player1 != null && player2 != null)
                            {

                                var games = new List<Game>();
                                for (int j = 1; j < matchEntry.Length - 2; j++)
                                {
                                    var items = matchEntry[j].Replace(",", "").
                                        Replace("Lazlo", "Laszlo").Replace("Shadwyn", "Ylaya").Replace("Gilrean", "Gilraen").Replace("Vlad", "Vladimir").
                                        Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);


                                    if (items[0] != "?" && items[4] != "?")
                                    {

                                        var faction1Id = factionDictEng[items[0]];
                                        var hero1 = items[1];
                                        if (hero1 == "?" && faction1Id == 1)
                                            hero1 = "Temkhan";
                                        else if (hero1 == "?" && faction1Id == 6)
                                            hero1 = "Arthas";

                                        var faction2Id = factionDictEng[items[3]];
                                        var hero2 = items[4];
                                        if (hero2 == "?" && faction1Id == 1)
                                            hero2 = "Temkhan";
                                        else if (hero2 == "?" && faction1Id == 6)
                                            hero2 = "Arthas";

                                        var game = new Game
                                        {
                                            IsConfirmed = true,

                                            Faction1Id = factionDictEng[items[0]],
                                            Hero1Id = context.Heroes.FirstOrDefault(h => h.NameEng == hero1).Id,

                                            ReportingPlayerWon = items[2] == ">",

                                            Faction2Id = factionDictEng[items[3]],
                                            Hero2Id = context.Heroes.FirstOrDefault(h => h.NameEng == hero2).Id,

                                            DateSubmitted = DateTime.ParseExact(matchEntry[matchEntry.Length - 1], "yyyy.MM.dd", null),
                                        };

                                        games.Add(game);

                                    }

                                }

                                var tournamentPlayer = tournamentPlayers.FirstOrDefault(tp => tp.Player == player1);
                                if (tournamentPlayer == null)
                                {
                                    tournamentPlayer = new TournamentPlayer
                                    {
                                        Player = player1
                                    };
                                    tournamentPlayers.Add(tournamentPlayer);
                                }

                                if (!tournamentGroupPlayers.Any(tgp => tgp.TournamentPlayer == tournamentPlayer))
                                    tournamentGroupPlayers.Add(new TournamentGroupPlayer { TournamentPlayer = tournamentPlayer });

                                tournamentPlayer = tournamentPlayers.FirstOrDefault(tp => tp.Player == player2);
                                if (tournamentPlayer == null)
                                {
                                    tournamentPlayer = new TournamentPlayer
                                    {
                                        Player = player2
                                    };
                                    tournamentPlayers.Add(tournamentPlayer);
                                }

                                if (!tournamentGroupPlayers.Any(tgp => tgp.TournamentPlayer == tournamentPlayer))
                                    tournamentGroupPlayers.Add(new TournamentGroupPlayer { TournamentPlayer = tournamentPlayer });


                                var date = matchEntry[matchEntry.Length - 1];
                                var score = matchEntry[matchEntry.Length - 2].Split(' ');

                                var match = new Match
                                {
                                    Player1 = player1,
                                    Player2 = player2,

                                    Games = games,

                                    IsFinished = true,

                                    DateFinished = DateTime.ParseExact(date, "yyyy.MM.dd", null),

                                    Player1Score = int.Parse(score[0]),
                                    Player2Score = int.Parse(score[2]),

                                };

                                matches.Add(match);

                            }         

                        }

                        group.Matches = matches;
                        group.TournamentGroupPlayers = tournamentGroupPlayers;

                        tournamentGroups.Add(group);

                    }

                    tournament.TournamentPlayers = tournamentPlayers;
                    tournament.TournamentGroups = tournamentGroups;

                    tournaments.Add(tournament);
                };

                return tournaments;

            }



        }

    }

}

