using RTA_Project_BL.Models;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RTA_Project_MVC.Helpers
{
    public interface IStatsHelper
    {
        StatisticsAllFactionsModel RecoverAllFactionsStats(IEnumerable<GameBL> filteredGames);
        StatisticsFactionHeroesModel RecoverFactionHeroesStats(IEnumerable<GameBL> filteredGames, int factionId, int[] heroesId);
    }

    public class StatsHelper : IStatsHelper
    {
        public StatisticsAllFactionsModel RecoverAllFactionsStats(IEnumerable<GameBL> filteredGames)
        {
            int[,] gamesPlayed = new int[8, 9];
            int[,] gamesWon = new int[8, 9];
            int[,] gamesLost = new int[8, 9];
            double[,] winrate = new double[8, 9];

            foreach (var game in filteredGames)
            {
                int fact1 = game.Faction1Id - 1, fact2 = game.Faction2Id - 1;
                gamesPlayed[fact1, fact2]++;
                gamesPlayed[fact2, fact1]++;

                if (game.Player1Won)
                    gamesWon[fact1, fact2]++;
                else
                    gamesWon[fact2, fact1]++;
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int games = gamesPlayed[i, j];
                    int wins = gamesWon[i, j];
                    int loses = games - wins;
                    gamesLost[i, j] = loses;
                    winrate[i, j] = GetWinPercent(games, wins);

                    gamesPlayed[i, 8] += games;
                    gamesWon[i, 8] += wins;
                    gamesLost[i, 8] += loses;
                }
                winrate[i, 8] = GetWinPercent(gamesPlayed[i, 8], gamesWon[i, 8]);
            }

            var statsModel = new StatisticsAllFactionsModel()
            {
                GamesPlayed = gamesPlayed,
                GamesWon = gamesWon,
                GamesLost = gamesLost,
                Winrate = winrate
            };

            return statsModel;

        }

        public StatisticsFactionHeroesModel RecoverFactionHeroesStats(IEnumerable<GameBL> filteredGames, int factionId, int[] heroesId)
        {

            int[] gamesPlayed = new int[8];
            int[] gamesWon = new int[8];
            int[] gamesLost = new int[8];
            double[] winrate = new double[8];

            Dictionary<int, (int games, int wins)[]> heroStatsDict = heroesId.ToDictionary(x => x, x => new (int games, int wins)[8]);

            foreach (var game in filteredGames)
            {
                int fact1 = game.Faction1Id;
                int fact2 = game.Faction2Id;
                int hero1 = game.Hero1Id;
                int hero2 = game.Hero2Id;

                int oppFact = fact1 != factionId ? fact1 : fact2;
                int oppIndex = oppFact - 1;

                gamesPlayed[oppIndex]++;

                if (oppFact == factionId)
                    gamesPlayed[oppIndex]++;

                if (game.Player1Won)
                {
                    if (game.Faction2Id == oppFact)
                        gamesWon[oppIndex]++;

                    if (heroStatsDict.ContainsKey(hero1))
                    {
                        var (games, wins) = heroStatsDict[hero1][oppIndex];
                        games++;
                        wins++;
                        heroStatsDict[hero1][oppIndex] = (games, wins);
                    }
                    if (heroStatsDict.ContainsKey(hero2))
                    {
                        var (games, wins) = heroStatsDict[hero2][oppIndex];
                        games++;
                        heroStatsDict[hero2][oppIndex] = (games, wins);
                    }
                }

                else
                {
                    if (game.Faction1Id == oppFact)
                        gamesWon[oppIndex]++;

                    if (heroStatsDict.ContainsKey(hero1))
                    {
                        var (games, wins) = heroStatsDict[hero1][oppIndex];
                        games++;
                        heroStatsDict[hero1][oppIndex] = (games, wins);
                    }
                    if (heroStatsDict.ContainsKey(hero2))
                    {
                        var (games, wins) = heroStatsDict[hero2][oppIndex];
                        games++;
                        wins++;
                        heroStatsDict[hero2][oppIndex] = (games, wins);
                    }
                }

            }


            var gamesPlayedHero = new List<int[]>();
            var gamesWonHero = new List<int[]>();
            var gamesLostHero = new List<int[]>();
            var winrateHero = new List<double[]>();

            foreach (var hero in heroStatsDict)
            {
                int[] gp = new int[8], gw = new int[8], gl = new int[8];
                double[] wr = new double[8];

                for (int i = 0; i < 8; i++)
                {
                    int games = hero.Value[i].games;
                    int wins = hero.Value[i].wins;
                    gp[i] = games;
                    gw[i] = wins;
                    gl[i] = games - wins;
                    wr[i] = GetWinPercent(games, wins);
                }

                gamesPlayedHero.Add(gp);
                gamesWonHero.Add(gw);
                gamesLostHero.Add(gl);
                winrateHero.Add(wr);
            }



            for (int i = 0; i < 8; i++)
            {
                gamesLost[i] = gamesPlayed[i] - gamesWon[i];
                winrate[i] = GetWinPercent(gamesPlayed[i], gamesWon[i]);
            }

            var statsModel = new StatisticsFactionHeroesModel()
            {
                GamesVsFactionPlayed = gamesPlayed,
                GamesVsFactionWon = gamesWon,
                GamesVsFactionLost = gamesLost,
                WinrateVsFaction = winrate,

                GamesVsFactionPlayedHero = gamesPlayedHero,
                GamesVsFactionWonHero = gamesWonHero,
                GamesVsFactionLostHero = gamesLostHero,
                WinrateVsFactionHero = winrateHero,
            };

            return statsModel;
        }

        private double GetWinPercent(int games, int wins)
        {
            return games == 0 ? 0 : Math.Round((double)(wins * 100) / games, 2);
        }
    }
}