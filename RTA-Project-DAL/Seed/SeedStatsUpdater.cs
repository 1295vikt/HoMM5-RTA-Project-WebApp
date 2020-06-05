using RTA_Project_DAL.Models;

namespace RTA_Project_DAL.Seed
{
    static class SeedStatsUpdater
    {
        public static void UpdateStats(PlayerStats stats1, PlayerStats stats2, byte faction1Id, byte faction2Id, bool player1Won)
        {
            stats1.GamesPlayed++;
            stats2.GamesPlayed++;
            if (player1Won)
                stats1.GamesWon++;
            else
                stats2.GamesWon++;

            switch (faction1Id)
            {
                case 1:
                    stats1.GamesAsAcademy++;
                    if (player1Won)
                        stats1.WinsAsAcademy++;
                    break;

                case 2:
                    stats1.GamesAsDungeon++;
                    if (player1Won)
                        stats1.WinsAsDungeon++;
                    break;

                case 3:
                    stats1.GamesAsFortress++;
                    if (player1Won)
                        stats1.WinsAsFortress++;
                    break;

                case 4:
                    stats1.GamesAsHaven++;
                    if (player1Won)
                        stats1.WinsAsHaven++;
                    break;

                case 5:
                    stats1.GamesAsInferno++;
                    if (player1Won)
                        stats1.WinsAsInferno++;
                    break;

                case 6:
                    stats1.GamesAsNecropolis++;
                    if (player1Won)
                        stats1.WinsAsNecropolis++;
                    break;

                case 7:
                    stats1.GamesAsStronghold++;
                    if (player1Won)
                        stats1.WinsAsStronghold++;
                    break;

                case 8:
                    stats1.GamesAsSylvan++;
                    if (player1Won)
                        stats1.WinsAsSylvan++;
                    break;
            }

            switch (faction2Id)
            {
                case 1:
                    stats2.GamesAsAcademy++;
                    if (!player1Won)
                        stats2.WinsAsAcademy++;
                    break;

                case 2:
                    stats2.GamesAsDungeon++;
                    if (!player1Won)
                        stats2.WinsAsDungeon++;
                    break;

                case 3:
                    stats2.GamesAsFortress++;
                    if (!player1Won)
                        stats2.WinsAsFortress++;
                    break;

                case 4:
                    stats2.GamesAsHaven++;
                    if (!player1Won)
                        stats2.WinsAsHaven++;
                    break;

                case 5:
                    stats2.GamesAsInferno++;
                    if (!player1Won)
                        stats2.WinsAsInferno++;
                    break;

                case 6:
                    stats2.GamesAsNecropolis++;
                    if (!player1Won)
                        stats2.WinsAsNecropolis++;
                    break;

                case 7:
                    stats2.GamesAsStronghold++;
                    if (!player1Won)
                        stats2.WinsAsStronghold++;
                    break;

                case 8:
                    stats2.GamesAsSylvan++;
                    if (!player1Won)
                        stats2.WinsAsSylvan++;
                    break;
            }

        }
    }
}
