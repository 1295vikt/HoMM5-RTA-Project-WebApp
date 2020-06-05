using RTA_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RTA_Project_DAL.Seed
{
    static class HLReportParser
    {
        public static List<Tournament> ParseHLReport(string filepath, RTADatabaseContext context)
        {

            var tournaments = new List<Tournament>();
            var data = File.ReadAllText(filepath).Split(new[] { "@tourn@" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var entry in data)
            {

                var tournEntry = entry.Split(new[] { "@group@" }, StringSplitOptions.RemoveEmptyEntries);

                var tournamentGroups = new List<TournamentGroup>();
                var tournamentPlayers = new List<TournamentPlayer>();

                var tournData = tournEntry[0].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                var mapVersion = tournData[7];
                var map = context.Maps.FirstOrDefault(m => m.Version == mapVersion);
                if (map == null)
                {
                    map = new Map { Version = mapVersion };
                    context.Maps.Add(map);
                }

                var tournament = new Tournament
                {
                    IsFinished = true,
                    IsOfficial = true,
                    IsArchived = true,

                    NameRus = tournData[0],
                    NameEng = tournData[1],
                    Year = int.Parse(tournData[2]),
                    IsSeasonal = bool.Parse(tournData[3]),
                    Season = byte.Parse(tournData[4]),
                    IsPrivate = bool.Parse(tournData[5]),
                    DateCreated = DateTime.ParseExact(tournData[6], "yyyy.MM.dd", null),

                    Map = map,

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
                        var matchEntry = line.Replace("[confirmed]", "").Replace("[/confirmed]", "").
                            Replace("[open]", "").Replace("[/open]", "").Replace("[denied]", "").Replace("[/denied]", "").Split(';');

                        var players = matchEntry[0].Split(' ');

                        var player1Name = players[0];
                        var player2Name = players[2];

                        var player1 = context.Players.FirstOrDefault(p => p.Name == player1Name);
                        if (player1 == null)
                        {
                            player1 = new Player
                            {
                                Name = player1Name,
                                GuidKey = Guid.NewGuid().ToString(),
                                Stats = new PlayerStats
                                {
                                    RatingClass = "B",
                                    RatingPointsCurrent = 1200,
                                    RatingPointsMax = 1200
                                }
                            };
                            context.Players.Add(player1);
                        }

                        var player2 = context.Players.FirstOrDefault(p => p.Name == player2Name);
                        if (player2 == null)
                        {
                            player2 = new Player
                            {
                                Name = player2Name,
                                GuidKey = Guid.NewGuid().ToString(),
                                Stats = new PlayerStats
                                {
                                    RatingClass = "B",
                                    RatingPointsCurrent = 1200,
                                    RatingPointsMax = 1200
                                }
                            };
                            context.Players.Add(player2);
                        }

                        var games = new List<Game>();
                        for (int j = 1; j < matchEntry.Length - 2; j++)
                        {
                            var items = matchEntry[j].Replace(",", "").
                                Replace("Lazlo", "Laszlo").Replace("Shadwyn", "Ylaya").Replace("Gilrean", "Gilraen").Replace("Vlad", "Vladimir").Replace("Timerkhan", "Temkhan").
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


                                if (hero1 != "?" && hero2 != "?")
                                {
                                    bool player1Won = items[2] == ">";

                                    var game = new Game
                                    {
                                        IsConfirmed = true,

                                        Faction1Id = faction1Id,
                                        Hero1Id = context.Heroes.FirstOrDefault(h => h.NameEng == hero1).Id,

                                        Player1Won = player1Won,

                                        Faction2Id = faction2Id,
                                        Hero2Id = context.Heroes.FirstOrDefault(h => h.NameEng == hero2).Id,

                                        DateSubmitted = DateTime.ParseExact(matchEntry[matchEntry.Length - 1], "yyyy.MM.dd", null),
                                    };

                                    SeedStatsUpdater.UpdateStats(player1.Stats, player2.Stats, faction1Id, faction2Id, player1Won);
                                    context.SaveChanges();

                                    games.Add(game);
                                }

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

        // Factions from HLReport
        private static Dictionary<string, byte> factionDictEng = new Dictionary<string, byte>
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

    }
}
