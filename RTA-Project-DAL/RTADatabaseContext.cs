using RTA_Project_DAL.Models;
using RTA_Project_DAL.Seed;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RTA_Project_DAL
{

    public class RTADatabaseContext : DbContext
    {
        public RTADatabaseContext() : base(@"Data Source = .\SQLEXPRESS;integrated security = true; initial catalog = RTA")
        {
            Database.SetInitializer(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentGroup> TournamentGroups { get; set; }
        public DbSet<TournamentPlayer> TournamentPlayers { get; set; }
        public DbSet<TournamentBracket> TournamentBrackets { get; set; }
        public DbSet<TournamentDescription> TournamentDescriptions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Map> Maps { get; set; }

    }
}
