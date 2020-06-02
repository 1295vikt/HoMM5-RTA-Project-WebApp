using RTA_Project_BL.Models;
using RTA_Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RTA_Project_MVC.Helpers
{
    public interface IStatsHelper
    {
        StatisticsViewModel MapToFactionStats(IEnumerable<GameBL> filteredGames, int factionId, int[] heroesId);
    }

    public class StatsHelper : IStatsHelper
    {

        public StatisticsViewModel MapToFactionStats(IEnumerable<GameBL> filteredGames, int factionId, int[] heroesId)
        {

            int[] gamesPlayed = new int[8];
            int[] gamesWon = new int[8];
            double[] winrate = new double[8];

            var statsModel = new StatisticsViewModel();

            //Key = [HeroId], Value = [opposing faction](games,wins)
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

            for (int i = 0; i < 8; i++)
                winrate[i] = GetWinPercent(gamesPlayed[i], gamesWon[i]);

            statsModel.HeroStats = heroStatsDict;


            winrate[factionId - 1] = 50;

            statsModel.GamesVsFactionPlayed = gamesPlayed;
            statsModel.GamesVsFactionWon = gamesWon;
            statsModel.WinrateVsFaction = winrate;

            return statsModel;

        }


        private double GetWinPercent(int games, int wins)
        {
            return games == 0 ? 0 : Math.Round((double)(wins * 100) / games, 2);
        }
    }
}